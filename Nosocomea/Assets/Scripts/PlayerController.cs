using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Values")]
    public float walkSpeed;
    public float turnSpeed; //represents degrees per second
    public float dashLength; //represents time (seconds) of dash
    public float dashPower;

    private Rigidbody rb;
    private Vector3 inputVector;

    private AttackBehavior attack;

    //Dash
    private bool dashing;
    private Vector3 storedDashVelocity;

    private PlayerStatController statController;
    private SoundPlayer soundPlayer;

    void Start()
    {
        statController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerStatController>();
        soundPlayer = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundPlayer>();
        rb = GetComponent<Rigidbody>();
        attack = GetComponent<AttackBehavior>();
        dashing = false;
    }

    private void Update()
    {
        GetInput();
        Look();
        Attack();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        //Get raw input from the player
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        //Check for dash input
        if (Input.GetKeyDown(KeyCode.LeftShift) && inputVector != Vector3.zero && !isDashing())
        {
            soundPlayer.PlaySound("dash");
            Dash();
        }
    }

    private void Look()
    {
        //Only do this if we are pressing a direction to avoid snapping to original rotation
        if (inputVector != Vector3.zero && !isDashing())
        {
            //In order to achieve proper ISO movement, we need to offset them by a certain angle    
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            //Need to invert input here for some reason
            var offsetInput = matrix.MultiplyPoint3x4(-inputVector);

            //The difference between where we are currently facing and where we want to be facing based on input
            Vector3 relativeDistance = (transform.position + offsetInput) - transform.position;
            //The rotation to apply given relativeDistance and vector to rotate around (in this case its the Y axis up vector)
            var rotation = Quaternion.LookRotation(relativeDistance, Vector3.up);


            //Smoothly rotate between current rotation and target rotation with a given turn speed
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }
    }

    private void Move()
    {
        if (dashing)
        {
            //If the player is dashing, move very fast in the stored direction pre dash
            rb.velocity = transform.forward * storedDashVelocity.normalized.magnitude * dashPower * Time.deltaTime;
        } else
        {
            //Moves the rigidbody forward multiplying it by the magnitude of the input vector (so if you dont press a key it doesn't move
            //as well as the speed and time step. 
            rb.velocity = transform.forward * inputVector.normalized.magnitude * CalcMoveSpeed() * Time.deltaTime;
        }     
    }

    private float CalcMoveSpeed()
    {
        float multiplier = Mathf.Lerp(1, 3, Mathf.InverseLerp(1, 20, statController.speed));
        return walkSpeed * multiplier;
    }

    private void Attack()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            attack.Attack();
        }
    }

    //Used externally to check if the player is currently dashing
    public bool isDashing()
    {
        return dashing;
    }

    //Store the pre-dash move vector, disable player movement and start dash timer
    private void Dash()
    {
        StartCoroutine(DashTimer());
        storedDashVelocity = inputVector;
    }

    //Wait until dash is finished then return player control
    private IEnumerator DashTimer()
    {
        dashing = true;
        yield return new WaitForSecondsRealtime(dashLength);
        dashing = false;
    }
}

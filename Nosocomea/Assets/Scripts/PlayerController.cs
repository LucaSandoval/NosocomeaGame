using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Values")]
    public float walkSpeed;
    public float turnSpeed; //represents degrees per second

    private Rigidbody rb;
    private Vector3 inputVector;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        GetInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        //Get raw input from the player
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Look()
    {
        //Only do this if we are pressing a direction to avoid snapping to original rotation
        if (inputVector != Vector3.zero)
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
        //Moves the rigidbody forward multiplying it by the magnitude of the input vector (so if you dont press a key it doesn't move
        //as well as the speed and time step. 
        rb.MovePosition(transform.position + transform.forward * inputVector.normalized.magnitude * walkSpeed * Time.deltaTime);
    }
}

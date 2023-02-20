using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    Transform player;

    Rigidbody rb;

    [SerializeField]
    float moveSpeed = 1f;

    [SerializeField]
    float minDistance = 2f;

    [SerializeField]
    int damageAmount = 1;

    [Header("Used for detection radius")]
    [SerializeField]
    float maxDistance = 20f;

    float distance;
    bool detected;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        rb = GetComponent<Rigidbody>();
        detected = false;
    }

    // Update is called once per frame
    void Update()
    {

        distance = Vector3.Distance(transform.position, player.position);
        if (distance < maxDistance && !detected)
        {
            detected = true;
        }
    }

    private void FixedUpdate()
    {
        if (detected) { 
            transform.LookAt(player);
            //float angle = Vector3.Angle(player.transform.position, transform.forward);
            //Vector3 vect = new Vector3(0,angle,0);
            //rb.AddTorque(vect);
            if (distance > minDistance)
            {
                rb.AddForce(transform.forward * moveSpeed);
            } 
        }
    }
}

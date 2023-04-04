using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody rb;

    private GameObject player;
    private Vector3 playerMoveVector;

    [HideInInspector]
    public Vector3 moveVector;
    [HideInInspector]
    public float moveSpeed;
    [HideInInspector]
    public float sizeMult;
    [HideInInspector]
    public bool trackPlayer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5);

        playerMoveVector = (player.transform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * moveSpeed;

        transform.localScale = new Vector3(1 * sizeMult, 1 * sizeMult, 1 * sizeMult);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!other.gameObject.GetComponent<PlayerController>().isDashing())
            {
                PlayerHealthController script = other.gameObject.GetComponent<PlayerHealthController>();
                script.TryHitPlayer(1);
                Destroy(gameObject);
            }           
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAI : MonoBehaviour
{
    AnimationManager _animationManager;
    public SimpleAIState state;
    private Rigidbody rb;
    private GameObject player;
    [Header("Behavior")]
    public float chaseRange;
    public float stopRange;
    [Header("Speeds")]
    public float wanderSpeed;
    public float chaseSpeed;

    private float moveSpeed;
    private Vector3 startPos;
    private Vector3 randPos;
    private Vector3 targetPos;
    private bool moving;

    private EnemyProjectileController projectileController;
    private RoomMember roomMember;

    public void Start()
    {
        roomMember = GetComponent<RoomMember>();
        projectileController = GetComponent<EnemyProjectileController>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        _animationManager = GetComponent<AnimationManager>();
        state = SimpleAIState.Chase;
        startPos = transform.position;
        moveSpeed = wanderSpeed;
        InvokeRepeating("PickNewRandomPoint", 1, 2);
    }

    private void Update()
    {
        Animate();
        switch(state)
        {
            case SimpleAIState.Wander:
                moving = true;
                moveSpeed = wanderSpeed;
                projectileController.active = false;

                SetTargetPos(randPos);

                if (Vector3.Distance(transform.position, randPos) <= 1)
                {
                    moving = false;
                } else
                {
                    moving = true;
                }

                if (Vector3.Distance(transform.position, player.transform.position) <= chaseRange && roomMember.memberRoom.playerInRoom)
                {
                    state = SimpleAIState.Chase;
                }

                break;
            case SimpleAIState.Chase:
                moving = true;
                SetTargetPos(player.transform.position);
                moveSpeed = chaseSpeed;
                projectileController.active = true;

                if (Vector3.Distance(transform.position, targetPos) <= stopRange)
                {
                    state = SimpleAIState.Stop;
                }

                if (Vector3.Distance(transform.position, player.transform.position) >= chaseRange && !roomMember.memberRoom.playerInRoom)
                {
                    state = SimpleAIState.Wander;
                }

                break;
            case SimpleAIState.Stop:
                moving = false;
                projectileController.active = true;

                if (Vector3.Distance(transform.position, player.transform.position) >= stopRange)
                {
                    state = SimpleAIState.Chase;
                }
                break;
        }
    }

    void SetTargetPos(Vector3 pos)
    {
        targetPos = new Vector3(pos.x, transform.position.y, pos.z);
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            transform.LookAt(targetPos);
            rb.velocity = transform.forward * moveSpeed;
        }        
    }

    private void PickNewRandomPoint()
    {
        float range = 3;
        randPos = new Vector3(startPos.x + Random.Range(-range, range), transform.position.y, startPos.z + Random.Range(-range, range));
    }
    private void Animate()
    {
        if (moving)
        {
            _animationManager.UpdateAnimatorValues(1);
        } else
        {
            _animationManager.UpdateAnimatorValues(0);
        }
        
    }
}

[System.Serializable]
public enum SimpleAIState
{
    Wander,
    Chase,
    Stop
}

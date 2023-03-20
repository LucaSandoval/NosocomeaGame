using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAgent : MonoBehaviour
{
  public enum EnemyState
  {
    Idle,
    Chase,
    Attack,
    Dead,
  }

  [SerializeReference] private EnemyState currentState;
  private NavMeshAgent agent;

  [Header("Player Character")]
  [SerializeField] private Transform playerTransform;

  [Header("AI Variables")]
  [SerializeField] private float chaseDistance = 10f;
  [SerializeField] private float attackRange = 3f;

  [Header("Attack")]
  [SerializeField] private float attackCooldown = 1f;
  private float attackTimer;
  [SerializeField] private float attackDamage = 1f;

  // Start is called before the first frame update
  void Start()
  {
    if (playerTransform == null)
    {
      playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    agent = GetComponent<NavMeshAgent>();
    agent.updatePosition = false;

    currentState = EnemyState.Idle;

    attackTimer = 0;
  }

  // Update is called once per frame
  void Update()
  {
    switch (currentState)
    {
      case EnemyState.Idle:
        Idle();
        break;
      case EnemyState.Chase:
        Chase();
        break;
      case EnemyState.Attack:
        Attack();
        break;
      case EnemyState.Dead:
        Dead();
        break;
      default:
        break;
    }
  }

  void Idle()
  {
    if (Vector3.Distance(transform.position, playerTransform.position) < chaseDistance)
    {
      currentState = EnemyState.Chase;
      return;
    }

    agent.ResetPath();
  }

  void Chase()
  {
    if (Vector3.Distance(transform.position, playerTransform.position) > chaseDistance)
    {
      currentState = EnemyState.Idle;
      return;
    }
    else if (Vector3.Distance(transform.position, playerTransform.position) < attackRange)
    {
      currentState = EnemyState.Attack;
      return;
    }

    agent.SetDestination(playerTransform.position);
    LookAtPlayer();
  }

  void Attack()
  {
    if (Vector3.Distance(transform.position, playerTransform.position) > attackRange)
    {
      currentState = EnemyState.Chase;
      return;
    }

    LookAtPlayer();

    if (attackTimer <= 0)
    {
      ExecuteAttack();
      attackTimer = attackCooldown;
    }

    attackTimer -= Time.deltaTime;
  }

  void ExecuteAttack()
  {
    // TODO: Execute the actual attack with animation and sound
  }

  void Dead()
  {
    // TODO: Don't simply destroy the player
    Destroy(gameObject);
  }

  void LookAtPlayer()
  {
    transform.rotation = Quaternion.Slerp(
        transform.rotation,
        Quaternion.LookRotation(playerTransform.position - transform.position),
        Time.deltaTime * 5f);
  }

  public EnemyState GetCurrentState()
  {
    return currentState; 
  }

  public bool ShouldMove()
  {
    return currentState == EnemyState.Chase || currentState == EnemyState.Attack;
  }

  public void Move()
  {
    Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
    Vector3 velocity = Vector3.zero;
    Vector2 smoothDeltaPosition = Vector2.zero;

    float dx = Vector3.Dot (transform.right, worldDeltaPosition);
    float dy = Vector3.Dot (transform.forward, worldDeltaPosition);

    Vector2 deltaPosition = new Vector2 (dx, dy);

    float smooth = Mathf.Min(1.0f, Time.deltaTime/0.15f);
    smoothDeltaPosition = Vector2.Lerp (smoothDeltaPosition, deltaPosition, smooth);

    if (Time.deltaTime > 1e-5f)
      velocity = smoothDeltaPosition / Time.deltaTime;
  }
}

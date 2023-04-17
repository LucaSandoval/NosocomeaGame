using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyAgent : MonoBehaviour
{

  [SerializeReference] private EnemyState currentState;
  private NavMeshAgent agent;

  private Rigidbody rb;

  [Header("Player Character")]
  [SerializeField] private Transform playerTransform;

  [Header("Navigation Variables")]
  [SerializeField] private float chaseDistance = 10f;
  [SerializeField] private float attackRange = 3f;
  [SerializeField] private float turnSpeed = 5f;
  // NOTE: Speed is controlled via the NavMeshAgent.

  [Header("Attack")]
  [SerializeField] private float attackCooldown = 1f;
  private float attackTimer;
  [SerializeField] private float attackDamage = 1f;

  private AnimationManager animationManager;

  // Start is called before the first frame update
  void Start()
  {
    if (playerTransform == null)
    {
      playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    rb = GetComponent<Rigidbody>();

    agent = GetComponent<NavMeshAgent>();
    agent.updatePosition = false;
    agent.updateRotation = false;

    currentState = EnemyState.Idle;

    animationManager = GetComponent<AnimationManager>();

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
    animationManager.UpdateAnimatorValues(0);
    if (Vector3.Distance(transform.position, playerTransform.position) <= chaseDistance)
    {
      currentState = EnemyState.Chase;
      return;
    }

    agent.ResetPath();
    Vector3 target = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime);
    rb.velocity = new Vector3(target.x, rb.velocity.y, target.y);
  }

  void Chase()
  {
    animationManager.UpdateAnimatorValues(1);
    if (Vector3.Distance(transform.position, playerTransform.position) > chaseDistance)
    {
      currentState = EnemyState.Idle;
      return;
    }
    else if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
    {
      currentState = EnemyState.Attack;
      return;
    }

    Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, chaseDistance);

    if (hit.collider != null && !hit.collider.CompareTag("Player"))
    {
      currentState = EnemyState.Idle;
      return;
    }

    agent.SetDestination(playerTransform.position);

    Vector3 targetVelocity = (agent.nextPosition - transform.position).normalized * agent.speed;
    rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);

    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(targetVelocity.x, 0, targetVelocity.z));
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

    agent.nextPosition = transform.position;
  }

  void Attack()
  {
    animationManager.UpdateAnimatorValues(0);
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
    rb.velocity = new Vector3(0, rb.velocity.y, 0);
  }

  void ExecuteAttack()
  {
    // TODO: Implement attack logic using the Damageable interface on the player
  }

  void Dead()
  {
    // TODO: Don't simply destroy the enemy
    Destroy(gameObject);
  }

  void LookAtPlayer()
  {
    transform.rotation = Quaternion.Slerp(
        transform.rotation,
        Quaternion.LookRotation(playerTransform.position - transform.position),
        Time.deltaTime * turnSpeed);
  }

  public EnemyState GetCurrentState()
  {
    return currentState; 
  }

  public void SetCurrentState(EnemyState newState)
  {
    currentState = newState;
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, chaseDistance);
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, attackRange);
    Gizmos.color = Color.cyan;
    Gizmos.DrawRay(transform.position, transform.forward * chaseDistance);
  }
}

[System.Serializable]
public enum EnemyState
{
  Idle,
  Chase,
  Attack,
  Dead,
}

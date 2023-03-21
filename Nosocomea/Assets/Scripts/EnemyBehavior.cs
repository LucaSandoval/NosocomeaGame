using UnityEngine;

[RequireComponent(typeof(EnemyAgent))]
public class EnemyBehavior : MonoBehaviour, Damageable
{
  [SerializeField] private float damage = 10f;
  [SerializeField] private float health = 50f;

  private EnemyAgent agent;
  private PlayerController player;

  // Start is called before the first frame update
  void Start()
  {
  }

  public void Attack(Damageable damageable)
  {
    damageable.ApplyDamage(damage);
  }

  public void ApplyDamage(float damageTaken)
  {
    health -= damageTaken;
    if (health <= 0)
    {
      GetComponent<EnemyAgent>().SetCurrentState(EnemyAgent.EnemyState.Dead);
    }
  }
}

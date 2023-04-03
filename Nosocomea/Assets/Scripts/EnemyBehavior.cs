using UnityEngine;

[RequireComponent(typeof(EnemyAgent))]
public class EnemyBehavior : AbstractDamageable
{
  [SerializeField] private float damage = 10f;
  [SerializeField] private float startingHealth = 50f;

  private EnemyAgent agent;
  private PlayerController player;

  // Start is called before the first frame update
  void Start()
  {
    health = startingHealth;
  }

  public void Attack(Damageable damageable)
  {
    damageable.ApplyDamage(damage);
  }
}

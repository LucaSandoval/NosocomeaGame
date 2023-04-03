using UnityEngine;

public abstract class AbstractDamageable : MonoBehaviour, Damageable
{
  protected float health;

  public void ApplyDamage(float damage)
  {
    health -= damage;
  }

  public float GetCurrentHealth()
  {
    return health;
  }

  public bool IsDestroyed()
  {
    return health <= 0;
  }

    public void SetHealth(float ammount)
    {
        health = ammount;
    }
}

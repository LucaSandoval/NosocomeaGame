// Represents a damageable object
public interface Damageable
{
  // Apply an amount of damage to the object
  void ApplyDamage(float damage);

  // Get the current health of the object
  float GetCurrentHealth();

  // Is the object destroyed?
  bool IsDestroyed();
}

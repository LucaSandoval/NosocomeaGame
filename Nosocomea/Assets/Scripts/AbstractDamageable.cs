using UnityEngine;

public abstract class AbstractDamageable : MonoBehaviour, Damageable
{
  protected float health;
    protected SoundPlayer soundPlayer;

    public void ApplyDamage(float damage)
  {
        soundPlayer = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundPlayer>();
        soundPlayer.PlaySound("hit");
        //PopupTextController.SpawnPopupText(damage.ToString(), transform.localPosition);
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

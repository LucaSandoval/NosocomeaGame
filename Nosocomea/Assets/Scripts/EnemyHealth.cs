using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : AbstractDamageable
{
    public float maxHealth;

    public void Start()
    {
        soundPlayer = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundPlayer>();
        SetHealth(maxHealth);
    }

    public void Update()
    {
        if (IsDestroyed())
        {
            soundPlayer.PlaySound("death");
            Destroy(gameObject);
        }
    }
}

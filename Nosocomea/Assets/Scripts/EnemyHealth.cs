using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : AbstractDamageable
{
    [SerializeField] private float maxHealth;
    [SerializeField] public float damage;

    private LevelManager levelManager;

    public void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelManager>();
        soundPlayer = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundPlayer>();


        maxHealth = Mathf.Round(Mathf.Lerp(8, 30, Mathf.InverseLerp(1, 10, levelManager.FloorCount)));
        damage = Mathf.Round(Mathf.Lerp(1, 10, Mathf.InverseLerp(1, 10, levelManager.FloorCount)));

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

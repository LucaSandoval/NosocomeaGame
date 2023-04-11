using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : AbstractDamageable
{

    private float maximumHealth;
    private PlayerStatController statController;
    private GameUI gameUI;

    public void Start()
    {
        statController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerStatController>();
        gameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUI>();

        maximumHealth = 20;
        SetHealth(maximumHealth);
    }

    private void Update()
    {
        if (IsDestroyed())
        {
            Die();
        }

        if (GetCurrentHealth() > maximumHealth)
        {
            SetHealth(maximumHealth);
        }

        maximumHealth = CalculateMaximumHealth();

        gameUI.healthBarSlider.maxValue = maximumHealth;
        gameUI.healthBarSlider.value = GetCurrentHealth();
        gameUI.healthBarNum.text = GetCurrentHealth() + "/" + maximumHealth;
    }

    public void TryHitPlayer(float damage)
    {
        float reducedDamage = damage * Mathf.Lerp(1, 0.1f, Mathf.InverseLerp(1, 20, statController.defence));
        reducedDamage = Mathf.Round(reducedDamage);


        //PopupTextController.SpawnPopupText(reducedDamage.ToString(), transform.localPosition);
        ApplyDamage(reducedDamage);
    }

    private float CalculateMaximumHealth()
    {
        return 18 + (statController.health * 2);
    }

    private void Die()
    {
        //do something idk
        LevelManager.instance.RestartLevel();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : AbstractDamageable
{

    private float maximumHealth;
    private PlayerStatController statController;
    private GameUI gameUI;

    private float hurtAlpha;

    public void Start()
    {
        statController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerStatController>();
        gameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUI>();

        hurtAlpha = 0;
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

    public void FixedUpdate()
    {
        if (hurtAlpha > 0)
        {
            hurtAlpha -= Time.deltaTime;
            gameUI.playerHurt.gameObject.SetActive(true);
            gameUI.playerHurt.color = new Color(gameUI.playerHurt.color.r, gameUI.playerHurt.color.g, gameUI.playerHurt.color.b, hurtAlpha);
        }
        else
        {
            gameUI.playerHurt.gameObject.SetActive(false);
        }
    }

    public void TryHitPlayer(float damage)
    {
        float reducedDamage = damage * Mathf.Lerp(1, 0.1f, Mathf.InverseLerp(1, 20, statController.defence));
        reducedDamage = Mathf.Round(reducedDamage);


        PopupTextController.SpawnPopupText(reducedDamage.ToString(), transform.localPosition);
        ApplyDamage(reducedDamage);
        hurtAlpha += 0.3f;
    }

    public float CalculateMaximumHealth()
    {
        return 18 + (statController.health * 2);
    }

    private void Die()
    {
        //do something idk
        LevelManager.instance.RestartLevel();
    }
}

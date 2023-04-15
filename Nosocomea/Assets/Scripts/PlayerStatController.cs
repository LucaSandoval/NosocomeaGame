using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatController : MonoBehaviour
{
    [Header("Player Stats")]
    public int speed;
    public int strength;
    public int reach;
    public int defence;
    public int health;
    public int quickness;

    public float critChance;
    public float critPower;

    private InventoryController inventoryController;
    private GameUI gameUI;

    private void Awake()
    {
        gameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUI>();
        inventoryController = GetComponent<InventoryController>();
    }

    public void Start()
    {
        SetStatsBlock(GetBaseStats());
    }

    public void Update()
    {
        gameUI.speedText.text = speed.ToString();
        gameUI.strengthText.text = strength.ToString();
        gameUI.reachText.text = reach.ToString();
        gameUI.defenceText.text = defence.ToString();
        gameUI.healthText.text = health.ToString();
        gameUI.quicknessText.text = quickness.ToString();
    }

    //stats the player starts a new run with
    public int[] GetBaseStats()
    {
        return new int[8] { 1, 1, 1, 1, 1, 1, 1, 50 };
    }

    //Set stats as a block
    public void SetStatsBlock(int[] stats)
    {
        speed = stats[0];
        strength = stats[1];
        reach = stats[2];
        defence = stats[3];
        health = stats[4];
        quickness = stats[5];
        critChance = stats[6];
        critPower = stats[7];
    }
}

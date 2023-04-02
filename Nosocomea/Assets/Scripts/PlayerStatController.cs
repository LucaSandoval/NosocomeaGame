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

    private InventoryController inventoryController;

    private void Awake()
    {
        inventoryController = GetComponent<InventoryController>();
    }

    public void Start()
    {
        SetStatsBlock(GetBaseStats());
    }

    //stats the player starts a new run with
    public int[] GetBaseStats()
    {
        return new int[6] { 1, 1, 1, 1, 1, 1 };
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
    }
}

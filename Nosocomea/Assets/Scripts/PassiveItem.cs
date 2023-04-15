using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Passive Item", menuName = "Items/Passive")]
public class PassiveItem : Item
{
    [Header("Stat Changes")]
    public int speed;
    public int strength;
    public int reach;
    public int defence;
    public int health;
    public int quickness;

    [Range(0, 100)] public int critChance;
    [Tooltip("Multiplier to base damage as percentage. 100 = 100% increase to damage")] 
    public int critPower;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Passive Item", menuName = "Items/Passive")]
public class PassiveItem : Item
{
    [Header("Stat Changes")]
    public float speed;
}

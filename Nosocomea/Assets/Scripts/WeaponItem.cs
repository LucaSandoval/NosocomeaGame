using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Items/Weapon")]
public class WeaponItem : Item
{
    [Header("Weapon Stats")]
    public int damage;
    public float attackSpeed;
    public float knockBack;
}

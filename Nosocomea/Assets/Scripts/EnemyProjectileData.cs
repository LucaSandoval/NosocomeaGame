using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Projectile Data", menuName = "Enemies/Projectile Data")]
public class EnemyProjectileData : ScriptableObject
{
    [Header("Bullet Info")]
    [Range(0.1f, 3)]
    public float size = 0.5f;
    [Range(0.1f, 10)]
    public float moveSpeed = 1f;
    [Header("Rate Info")]
    [Range(0.01f, 1)]
    public float fireRate = 1f; //realtime seconds
    public SpreadType spread;
    [Header("Extra")]
    public bool trackPlayer;
}

[System.Serializable]
public enum SpreadType
{
    single,
    fourRing,
    eightRing
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    public bool active;
    public Transform firePos;

    public EnemyProjectileAttack[] attacks;
    
    //Attack Switch
    private int attackID;
    private EnemyProjectileAttack currentAttack;
    private float maxTimer;
    private float curTimer;

    //Fire Data
    private float fireRateTimer;
    private float curFireRateTimer;

    private GameObject enemyProjectilePrefab;
    private SoundPlayer soundPlayer;
    private EnemyHealth enemyHealth;

    [System.Serializable]
    public struct EnemyProjectileAttack
    {
        public EnemyProjectileData data;
        public float length;
    }

    private void Start()
    {
        //active = false;
        soundPlayer = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundPlayer>();
        enemyProjectilePrefab = Resources.Load<GameObject>("EnemyProjectile");
        enemyHealth = GetComponent<EnemyHealth>();
        attackID = 0;
        SetCurrentAttack(0);
    }

    private void SetCurrentAttack(int id)
    {
        currentAttack = attacks[id];
        maxTimer = currentAttack.length;
        curTimer = maxTimer;

        fireRateTimer = currentAttack.data.fireRate;
        curFireRateTimer = fireRateTimer;
    }

    private void NextAttack()
    {
        attackID++;
        if (attackID >= attacks.Length)
        {
            attackID = 0;
        }

        SetCurrentAttack(attackID);
    }

    //Fires all bullets at the same time 
    private void PulseBullets()
    {
        soundPlayer.PlaySound("shoot");
        switch (currentAttack.data.spread)
        {
            case SpreadType.single:
                FireBullet(0);
                break;
            case SpreadType.fourRing:
                FireBullet(0);
                FireBullet(90);
                FireBullet(-90);
                FireBullet(-180);
                break;
            //case SpreadType.eightRing:
            //    FireBullet(transform.forward);
            //    FireBullet(Vector3.Lerp(transform.forward, -transform.right, 0.5f));
            //    FireBullet(-transform.right);
            //    FireBullet(Vector3.Lerp(-transform.right, transform.right, 0.5f));
            //    FireBullet(transform.right);
            //    FireBullet(Vector3.Lerp(transform.right, -transform.forward, 0.5f));
            //    FireBullet(-transform.forward);
            //    FireBullet(Vector3.Lerp(-transform.forward, transform.forward, 0.5f));
            //    break;
        }
    }

    private void FireBullet(float angle)
    {
        GameObject newBullet = Instantiate(enemyProjectilePrefab);
        newBullet.transform.position = firePos.position;

        if (angle != 0)
        {
            newBullet.transform.rotation = transform.rotation * Quaternion.Euler(0, angle, 0);
        } else
        {
            newBullet.transform.rotation = transform.rotation;
        }               
        
        EnemyProjectile script = newBullet.GetComponent<EnemyProjectile>();

        script.moveSpeed = currentAttack.data.moveSpeed;
        script.sizeMult = currentAttack.data.size;
        script.trackPlayer = currentAttack.data.trackPlayer;
        script.damage = enemyHealth.damage;

        if (currentAttack.data.trackPlayer)
        {
            newBullet.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 0.5f, 0));
        }
    }

    private void FixedUpdate()
    {
        if (active)
        {
            //Timer for switching attack pattern
            if (curTimer > 0)
            {
                curTimer -= Time.deltaTime;
            }
            else
            {
                NextAttack();
            }

            //Timer for firing bullets
            if (curFireRateTimer > 0)
            {
                curFireRateTimer -= Time.deltaTime;
            } else
            {
                curFireRateTimer = fireRateTimer;
                PulseBullets();
            }
        }        
    }
}

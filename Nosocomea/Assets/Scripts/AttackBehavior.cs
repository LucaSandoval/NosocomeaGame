using System.Collections;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject attackDisplay;
    [SerializeField] private Collider attackField;

    [Header("Attributes")]
    [SerializeField] private float attackLength;

    private SoundPlayer soundPlayer;
    private PlayerStatController statController;
    private InventoryController inventoryController;

    private bool canAttack;

    void Start()
    {
        soundPlayer = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundPlayer>();
        statController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerStatController>();
        inventoryController = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryController>();

        if (attackField == null)
        {
            attackField = GetComponentInChildren<Collider>();
        }

        canAttack = true;
        attackDisplay.SetActive(false);
    }

    public void Attack()
    {
        // Do nothing if the coroutine is already running
        if (canAttack == false)
        {
            return;
        }

        canAttack = false;

        soundPlayer.PlaySound("attack");
        attackDisplay.SetActive(true);

        // Destroy anything in the attack field
        Collider[] hitColliders = Physics.OverlapBox(attackField.bounds.center, attackField.bounds.size);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy") || hitCollider.gameObject.CompareTag("Projectile"))
            {
                EnemyHealth script = hitCollider.gameObject.GetComponent<EnemyHealth>();
                script.ApplyDamage(CalculateDamage());
            }
        }

        StartCoroutine(AttackRoutine());
    }

    private float CalculateDamage()
    {
        float multiplier = Mathf.Lerp(1, 5, Mathf.InverseLerp(1, 20, statController.strength));
        float baseDamage = 4;
        if (inventoryController.equippedWeapon != null)
        {
            baseDamage = inventoryController.equippedWeapon.damage;
        }

        return baseDamage * multiplier;
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSecondsRealtime(attackLength);
        attackDisplay.SetActive(false);

        float finalDelay = CalculateAttackSpeed() - attackLength;
        if (finalDelay < 0)
        {
            finalDelay = 0;
        }

        yield return new WaitForSecondsRealtime(finalDelay);
        canAttack = true;
    }


    private float CalculateAttackSpeed()
    {
        float multiplier = Mathf.Lerp(1, 0.2f, Mathf.InverseLerp(1, 20, statController.quickness));
        float baseSpeed = 0.75f;

        if (inventoryController.equippedWeapon != null)
        {
            baseSpeed = inventoryController.equippedWeapon.attackSpeed;
        }

        return baseSpeed * multiplier;
    }
}

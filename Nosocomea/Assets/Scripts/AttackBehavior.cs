using System.Collections;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject attackRotation;
    [SerializeField] private Collider attackField;
    private GameObject attackDisplayPrefab;

    [Header("Attributes")]
    [SerializeField] private float attackLength;

    private SoundPlayer soundPlayer;
    private PlayerStatController statController;
    private InventoryController inventoryController;

    private bool canAttack;
    private bool isCrit;

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
        attackDisplayPrefab = Resources.Load<GameObject>("AttackDisplayPrefab");
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
        

        //Set the rotation of the attack 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 clickPosition = hit.point;
            Vector3 targetPos = new Vector3(clickPosition.x, attackRotation.transform.position.y, clickPosition.z);
            attackRotation.transform.LookAt(targetPos);

            GameObject newAttackDisplay = Instantiate(attackDisplayPrefab);
            newAttackDisplay.transform.GetChild(0).GetComponent<AttackAnimation>().attackLength = attackLength;
            newAttackDisplay.transform.position = attackField.transform.position;
            newAttackDisplay.transform.rotation = attackField.transform.rotation;

            //Set size properly
            newAttackDisplay.transform.localScale = CalculateReachScale();
            attackField.transform.localScale = CalculateReachScale();
        }

        int randomChance = Random.Range(0, 100);
        isCrit = randomChance <= statController.critChance;

        // Destroy anything in the attack field
        Collider[] hitColliders = Physics.OverlapBox(attackField.bounds.center, attackField.bounds.size);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy") || hitCollider.gameObject.CompareTag("Projectile"))
            {
                EnemyHealth script = hitCollider.gameObject.GetComponent<EnemyHealth>();
                PopupTextController.SpawnPopupText(CalculateDamage().ToString(), hitCollider.gameObject.transform.position);
                script.ApplyDamage(CalculateDamage());
            }
        }

        StartCoroutine(AttackRoutine());
    }

    private float CalculateDamage()
    {
        float multiplier = Mathf.Lerp(1, 5, Mathf.InverseLerp(1, 20, statController.strength));
        float baseDamage = 4;
        float critMultiplier = 1;

        if (isCrit)
        {
            critMultiplier = 1 + Mathf.InverseLerp(0, 100, statController.critPower);
        }

        if (inventoryController.equippedWeapon != null)
        {
            baseDamage = inventoryController.equippedWeapon.damage;
        }

        return (int)(baseDamage * multiplier * critMultiplier);
    }

    private Vector3 CalculateReachScale()
    {
        float multiplier = Mathf.Lerp(1, 5, Mathf.InverseLerp(1, 20, statController.reach));
        float baseScale = 1;

        return new Vector3(baseScale * multiplier, baseScale * multiplier, baseScale * multiplier);
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSecondsRealtime(attackLength);

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

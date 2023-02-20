using System.Collections;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject attackDisplay;
    [SerializeField] private Collider attackField;

    [Header("Attributes")]
    [SerializeField] private float attackLength;

    void Start()
    {
        if (attackField == null)
        {
            attackField = GetComponentInChildren<Collider>();
        }

        attackDisplay.SetActive(false);
    }

    public void Attack()
    {
        // Do nothing if the coroutine is already running
        if (attackDisplay.activeSelf)
        {
            return;
        }

        attackDisplay.SetActive(true);

        // Destroy anything in the attack field
        Collider[] hitColliders = Physics.OverlapBox(attackField.bounds.center, attackField.bounds.size);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                Destroy(hitCollider.gameObject);
            }
        }

        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(attackLength);
        attackDisplay.SetActive(false);
    }
}

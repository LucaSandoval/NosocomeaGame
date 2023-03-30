using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    private float collectionRange = 2f;
    private GameObject player;
    protected InventoryController inventoryController;

    [HideInInspector]
    public bool preventPickupUntilReset;

    public virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventoryController = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryController>();
    }

    public virtual void Update()
    {
        if (preventPickupUntilReset)
        {
            if (!(Vector3.Distance(transform.position, player.transform.position) <= collectionRange))
            {
                preventPickupUntilReset = false;
            }
        } else
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= collectionRange)
            {
                Collect();
            }
        }      
    }

    //What happens when the player gets close enough to pick the item up
    public abstract void Collect();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Collectable
{
    public Item item;
    public override void Collect()
    {
        inventoryController.PickupItem(item);
        Destroy(gameObject);
    }
}

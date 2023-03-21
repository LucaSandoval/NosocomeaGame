using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Collectable
{
    public Item item;
    public AudioClip pickupSFX;
    public override void Collect()
    {
        inventoryController.PickupItem(item);
        AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Collectable
{
    public Item item;
    private SoundPlayer soundPlayer;
    public override void Collect()
    {
        soundPlayer = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundPlayer>();
        inventoryController.PickupItem(item);
        soundPlayer.PlaySound("pickup");
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public WeaponItem equippedWeapon;
    public List<Item> currentItems;

    private GameUI gameUI;
    private GameObject itemListPrefab;
    private GameObject itemSplashPrefab;
    private GameObject groundItemPrefab;

    private PlayerStatController statController;
    private Canvas mainCanvas;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        statController = GetComponent<PlayerStatController>();
        gameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUI>();
        itemListPrefab = Resources.Load<GameObject>("ItemListPrefab");
        itemSplashPrefab = Resources.Load<GameObject>("ItemSplash");
        //COME BACK AND NAME THIS BETTER
        groundItemPrefab = Resources.Load<GameObject>("TestItem");
        currentItems = new List<Item>();
    }

    private void Update()
    {
        CalculatePlayerStats();
        ProcessCurrentWeapon();
    }

    public void PickupItem(Item item)
    {
        GameObject newSplash = Instantiate(itemSplashPrefab);
        newSplash.transform.SetParent(mainCanvas.transform, false);
        newSplash.GetComponent<ItemSplash>().SetItem(item);

        //If this is a passive item, add it to the passive items list.
        //Otherwise, if its a weapon, replace the current weapon and the drop it. 
        if (item is WeaponItem)
        {           
            if(equippedWeapon != null)
            {
                //drop old weapon
                GameObject newDroppedWeapon = Instantiate(groundItemPrefab);
                ItemPickup pickupScript = newDroppedWeapon.GetComponent<ItemPickup>();
                pickupScript.item = equippedWeapon;
                pickupScript.preventPickupUntilReset = true;
                newDroppedWeapon.transform.position = player.transform.position;
            }

            equippedWeapon = (WeaponItem)item;

        } else
        {
            currentItems.Add(item);
            //WIP
            GameObject newListItem = Instantiate(itemListPrefab);
            newListItem.GetComponent<ItemListPrefab>().thisItem = item;
            newListItem.GetComponent<ItemListPrefab>().SetItemInfo();
            newListItem.transform.SetParent(gameUI.iventoryParentObject.transform, false);
        }  
    }

    //Update weapon stats or something idk yet
    public void ProcessCurrentWeapon()
    {
        if (equippedWeapon != null)
        {
            gameUI.weaponIcon.gameObject.SetActive(true);
            gameUI.weaponIcon.sprite = equippedWeapon.itemIcon;
        } else
        {
            gameUI.weaponIcon.gameObject.SetActive(false);
        }
    }

    //Goes through all (passive) player items and calculates what their current stat block should be
    public void CalculatePlayerStats()
    {
        if(currentItems.Count == 0)
        {
            return;
        }


        int[] finalStatBlock = statController.GetBaseStats();

        for(int i = 0; i < currentItems.Count; i++)
        {
            if (currentItems[i] is PassiveItem)
            {
                //Speed
                finalStatBlock[0] += ((PassiveItem)currentItems[i]).speed;
                //Strength
                finalStatBlock[1] += ((PassiveItem)currentItems[i]).strength;
                //Reach
                finalStatBlock[2] += ((PassiveItem)currentItems[i]).reach;
                //Defence
                finalStatBlock[3] += ((PassiveItem)currentItems[i]).defence;
                //Health
                finalStatBlock[4] += ((PassiveItem)currentItems[i]).health;
                //Quickness
                finalStatBlock[5] += ((PassiveItem)currentItems[i]).quickness;
                //CritChance
                finalStatBlock[6] += ((PassiveItem)currentItems[i]).critChance;
                //CritPower
                finalStatBlock[7] += ((PassiveItem)currentItems[i]).critPower;
            }
        }

        statController.SetStatsBlock(finalStatBlock);
    }
}



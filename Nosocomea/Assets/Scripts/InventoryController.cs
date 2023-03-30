using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<Item> currentItems;

    private GameUI gameUI;
    private GameObject itemListPrefab;
    private GameObject itemSplashPrefab;

    private PlayerStatController statController;
    private Canvas mainCanvas;

    void Start()
    {
        mainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        statController = GetComponent<PlayerStatController>();
        gameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUI>();
        itemListPrefab = Resources.Load<GameObject>("ItemListPrefab");
        itemSplashPrefab = Resources.Load<GameObject>("ItemSplash");
        currentItems = new List<Item>();
    }

    private void Update()
    {
        CalculatePlayerStats();
    }

    public void PickupItem(Item item)
    {
        currentItems.Add(item);
        //WIP
        GameObject newListItem = Instantiate(itemListPrefab);
        newListItem.GetComponent<ItemListPrefab>().thisItem = item;
        newListItem.GetComponent<ItemListPrefab>().SetItemInfo();
        newListItem.transform.SetParent(gameUI.iventoryParentObject.transform, false);

        GameObject newSplash = Instantiate(itemSplashPrefab);
        newSplash.transform.SetParent(mainCanvas.transform, false);
        newSplash.GetComponent<ItemSplash>().SetItem(item);
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
            }
        }

        statController.SetStatsBlock(finalStatBlock);
    }
}



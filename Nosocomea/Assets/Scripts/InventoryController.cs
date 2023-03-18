using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<Item> currentItems;
    private GameUI gameUI;
    private GameObject itemListPrefab;

    void Start()
    {
        gameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUI>();
        itemListPrefab = Resources.Load<GameObject>("ItemListPrefab");
        currentItems = new List<Item>();
    }

    public void PickupItem(Item item)
    {
        currentItems.Add(item);
        //WIP
        GameObject newListItem = Instantiate(itemListPrefab);
        newListItem.GetComponent<ItemListPrefab>().thisItem = item;
        newListItem.GetComponent<ItemListPrefab>().SetItemInfo();
        newListItem.transform.SetParent(gameUI.iventoryParentObject.transform, false);
    }
}

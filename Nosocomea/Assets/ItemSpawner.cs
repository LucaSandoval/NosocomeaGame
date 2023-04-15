using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Item[] items;
    private GameObject itemPrefab;

    public void Awake()
    {
        itemPrefab = Resources.Load<GameObject>("TestItem");

        int rand = Random.Range(0, items.Length);
        GameObject newItem = Instantiate(itemPrefab);
        newItem.transform.position = transform.position + new Vector3(0, 2f, 0);
        newItem.transform.SetParent(transform);

        newItem.GetComponent<ItemPickup>().item = items[rand];
    }
}

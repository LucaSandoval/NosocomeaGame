using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSplash : MonoBehaviour
{
    [HideInInspector]
    public Item curItem;
    public Text nameText;
    public Text descText;

    public void SetItem(Item item)
    {
        nameText.text = item.itemName;
        descText.text = item.itemDescription;
    }
}

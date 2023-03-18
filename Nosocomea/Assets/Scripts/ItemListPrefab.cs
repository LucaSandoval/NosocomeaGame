using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListPrefab : MonoBehaviour
{
    public Item thisItem;
    public Image iconImage;
    public Text nameText;

    // Update is called once per frame
    void Update()
    {
        SetItemInfo();
    }

    public void SetItemInfo()
    {
        if (thisItem != null)
        {
            iconImage.sprite = thisItem.itemIcon;
            nameText.text = thisItem.itemName;
        }
    }
}

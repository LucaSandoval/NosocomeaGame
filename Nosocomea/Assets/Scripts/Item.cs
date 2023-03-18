using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;
    [TextArea(3, 10)]
    public string itemDescription;
    public Sprite itemIcon;
}

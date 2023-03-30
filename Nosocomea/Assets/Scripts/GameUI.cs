using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Serves as a place to store refrences to important UI components in its children. Shouldn't do anything else.
public class GameUI : MonoBehaviour
{
    [Header("Inventory")]
    public GameObject iventoryParentObject;
    [Header("Equipped Weapon")]
    public Image weaponIcon;
}

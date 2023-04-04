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
    [Header("Stat Block")]
    public Text speedText;
    public Text strengthText;
    public Text reachText;
    public Text defenceText;
    public Text healthText;
    public Text quicknessText;
    [Header("Health Bar")]
    public Slider healthBarSlider;
    public Text healthBarNum;
    [Header("Level Win")]
    public GameObject levelWinObject;
}

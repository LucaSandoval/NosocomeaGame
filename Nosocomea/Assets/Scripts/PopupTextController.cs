using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupTextController : MonoBehaviour
{
    private static GameObject textPrefab;
    private static GameObject canvas;

    public static void SpawnPopupText(string text, Vector3 location)
    {
        canvas = GameObject.Find("Canvas");
        textPrefab = Resources.Load<GameObject>("TextPopUp");

        GameObject newText = Instantiate(textPrefab);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location);

        newText.transform.SetParent(canvas.transform, false);
        newText.transform.position = screenPosition;
        newText.transform.GetChild(0).GetComponent<Text>().text = text;
    }
}

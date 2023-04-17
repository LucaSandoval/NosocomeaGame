using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] ScreenFader statPanel;
    [SerializeField] ScreenFader blackPanel;
    void Start()
    {
        statPanel.SetAlpha(0f);
        blackPanel.SetAlpha(1.0f);
        blackPanel.FadeOut();
    }

    
}

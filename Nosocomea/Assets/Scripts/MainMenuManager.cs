using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] ElementFader statPanel;
    [SerializeField] TextMeshProUGUI gamesPlayedText;
    [SerializeField] ElementFader blackPanel;
    void Start()
    {
        statPanel.SetAlpha(0f);
        blackPanel.SetAlpha(1.0f);
        blackPanel.FadeOut();
        gamesPlayedText.text = PlayerPrefs.GetInt("GamesPlayed").ToString();
    }

    public void StatPanel()
    {
        if (statPanel.GetAlpha() == 0)
        {
            statPanel.FadeIn();
        } else if (statPanel.GetAlpha() == 1)
        {
            statPanel.FadeOut();
        }
    }
    public void PlayGame()
    {
        PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed") + 1);
        blackPanel.FadeIn(LoadGame, 0.5f);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        blackPanel.FadeIn(QuitApplication, 0.5f);
    }

    private void QuitApplication()
    {
        Application.Quit();
    }

}

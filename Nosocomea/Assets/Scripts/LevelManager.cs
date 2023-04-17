using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour, LevelChangeEventEmitter
{
    public int FloorCount;

    private GameUI gameUI;
    [HideInInspector]
    public float fadeAlpha;

  public static LevelManager instance;

  private int level;
  private List<LevelChangeEventListener> listeners;

  // Start is called before the first frame update
  void Start()
  {
    if (instance != null)
    {
      Destroy(gameObject);
    }

    instance = this;

    level = 0;

        gameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUI>();
        FloorCount = 1;
  }

    private void Update()
    {
        gameUI.floorCount.text = FloorCount.ToString("00");
    }

    private void FixedUpdate()
    {
        if (fadeAlpha > 0)
        {
            fadeAlpha -= Time.deltaTime;
            gameUI.sceneFade.gameObject.SetActive(true);
            gameUI.sceneFade.color = new Color(gameUI.sceneFade.color.r, gameUI.sceneFade.color.g, gameUI.sceneFade.color.b, fadeAlpha);
        } else
        {
            gameUI.sceneFade.gameObject.SetActive(false);
        }
    }

    public void RestartLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void OnLevelChange(int level)
  {
    foreach (LevelChangeEventListener listener in listeners)
    {
      listener.OnLevelChange(level);
    }
  }

  public void RegisterListener(LevelChangeEventListener listener)
  {
    this.listeners.Add(listener);
  }

  public void UnregisterListener(LevelChangeEventListener listener)
  {
    this.listeners.Remove(listener);
  }
}

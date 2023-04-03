using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour, LevelChangeEventEmitter
{
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

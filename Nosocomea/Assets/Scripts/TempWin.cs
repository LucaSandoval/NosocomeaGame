using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempWin : MonoBehaviour
{
    private GameObject player;
    private GameUI gameUI;
    private SoundPlayer soundPlayer;

    private bool won;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUI>();
        soundPlayer = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundPlayer>();
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 3 && won == false)
        {
            won = true;
            StartCoroutine(win());
        }
    }

    private IEnumerator win()
    {
        soundPlayer.PlaySound("win");
        gameUI.levelWinObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        LevelManager.instance.RestartLevel();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public NewRoom thisRoom;
    public GameObject[] possibleEnemies;

    void Awake()
    {
        int rand = Random.Range(0, possibleEnemies.Length);
        GameObject newEnemy = Instantiate(possibleEnemies[rand]);
        newEnemy.transform.position = transform.position + new Vector3(0, 0.5f, 0);

        newEnemy.transform.SetParent(transform);

        newEnemy.transform.GetChild(0).GetComponent<RoomMember>().memberRoom = thisRoom;
        thisRoom.currentEnemies.Add(newEnemy);
    }
}

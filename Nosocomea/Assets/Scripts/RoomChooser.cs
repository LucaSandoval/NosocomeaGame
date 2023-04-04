using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChooser : MonoBehaviour
{
  [Header("Room Weights")]
  [SerializeField] private float genericWeight;
  [SerializeField] private float lootWeight;
  [SerializeField] private float enemyWeight;

  [Header("Room Generation Prefabs")]
  [SerializeField] private List<GameObject> genericRooms;
  [SerializeField] private List<GameObject> lootRooms;
  [SerializeField] private List<GameObject> enemyRooms;
  [SerializeField] private List<GameObject> bossRooms;

  public List<GameObject> GenerateRooms(int rooms)
  {
    List<GameObject> roomsList = new List<GameObject>();

    for (int currentRoom = 1; currentRoom <= rooms; currentRoom++)
    {
      GameObject room = GetNextRoom(currentRoom, rooms);
      roomsList.Add(room);
    }

    return roomsList;
  }

  public GameObject GetNextRoom(int currentRoom, int rooms)
  {
    if (currentRoom >= rooms)
    {
      return GetRandomBossRoom();
    }

    currentRoom++;

    Vector3 weights = new Vector3(
      Random.Range(0f, genericWeight),
      Random.Range(0f, lootWeight),
      Random.Range(0f, enemyWeight));

    if (weights.x > weights.y && weights.x > weights.z)
    {
      return GetRandomGenericRoom();
    }
    else if (weights.y > weights.z)
    {
      return GetRandomLootRoom();
    }
    else
    {
      return GetRandomEnemyRoom();
    }
  }

  public GameObject GetRandomGenericRoom()
  {
    int randomIndex = Random.Range(0, genericRooms.Count);
    return genericRooms[randomIndex];
  }
  
  public GameObject GetRandomLootRoom()
  {
    int randomIndex = Random.Range(0, lootRooms.Count);
    return lootRooms[randomIndex];
  }

  public GameObject GetRandomEnemyRoom()
  {
    int randomIndex = Random.Range(0, enemyRooms.Count);
    return enemyRooms[randomIndex];
  }

  public GameObject GetRandomBossRoom()
  {
    int randomIndex = Random.Range(0, bossRooms.Count);
    return bossRooms[randomIndex];
  }
}

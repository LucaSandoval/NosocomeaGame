using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizerRoom : MonoBehaviour
{
  [Header("Room Generation Settings")]
  [SerializeField] private float xOffset = 0f; // X offset
  [SerializeField] private float yOffset = 0f; // Y offset
  [SerializeField] private float zOffset = 0f; // Z offset

  [Header("Room Weights")]
  [SerializeField] private float genericWeight;
  [SerializeField] private float lootWeight;
  [SerializeField] private float enemyWeight;

  [Header("Room Generation Prefabs")]
  [SerializeField] private List<GameObject> genericRooms;
  [SerializeField] private List<GameObject> lootRooms;
  [SerializeField] private List<GameObject> enemyRooms;
  [SerializeField] private List<GameObject> bossRooms;

  // void Start()
  // {
  //   // Choose a random prefab from the array
  //   int randomIndex = Random.Range(0, roomPrefabs.Length);
  //   GameObject roomPrefab = roomPrefabs[randomIndex];
  //
  //   // Get the position of the game object this script is attached to
  //   Vector3 position = transform.position;
  //
  //   // Add the specified offsets to the position
  //   position += new Vector3(offsetX, offsetY, offsetZ);
  //
  //   // Instantiate the prefab at the modified position
  //   Instantiate(roomPrefab, position, Quaternion.identity);
  // }
  
  List<GameObject> GenerateRooms(int rooms)
  {
    List<GameObject> roomsList = new List<GameObject>();

    for (int currentRoom = 0; currentRoom < rooms; currentRoom++)
    {
      GameObject room = GetNextRoom(currentRoom, rooms);
      roomsList.Add(room);
    }

    return roomsList;
  }

  GameObject GetNextRoom(int currentRoom, int rooms)
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

  GameObject GetRandomGenericRoom()
  {
    int randomIndex = Random.Range(0, genericRooms.Count);
    return genericRooms[randomIndex];
  }
  
  GameObject GetRandomLootRoom()
  {
    int randomIndex = Random.Range(0, lootRooms.Count);
    return lootRooms[randomIndex];
  }

  GameObject GetRandomEnemyRoom()
  {
    int randomIndex = Random.Range(0, enemyRooms.Count);
    return enemyRooms[randomIndex];
  }

  GameObject GetRandomBossRoom()
  {
    int randomIndex = Random.Range(0, bossRooms.Count);
    return bossRooms[randomIndex];
  }
}


using UnityEngine;
using System.Collections.Generic;

public class RoomSpawner : MonoBehaviour
{
public GameObject roomPrefab; // prefab of the room to spawn
public GameObject hallwayPrefab; // prefab of the hallway to spawn
public int rows = 3; // number of rows of rooms
public int columns = 3; // number of columns of rooms
public float roomSpacing = 10f; // distance between each room
public float hallwayLength = 10f; // length of each hallway
public float hallwayXOffset = 0f; // x offset for the hallway
public float hallwayYOffset = 0f; // y offset for the hallway
public float hallwayZOffset = 0f; // z offset for the hallway
public float hallwayXOffsetEW = 0f; // x offset for east-west hallways
public float hallwayYOffsetEW = 0f; // y offset for east-west hallways
public float hallwayZOffsetNS = 0f; // z offset for north-south hallways

private List<GameObject> spawnedRooms = new List<GameObject>(); // list to store spawned rooms

void Start()
{
    // spawn the rooms
    for (int row = 0; row < rows; row++)
    {
        for (int col = 0; col < columns; col++)
        {
            Vector3 roomPos = new Vector3(col * roomSpacing, 0, row * roomSpacing);
            GameObject currentRoom = Instantiate(roomPrefab, roomPos, Quaternion.identity);
            spawnedRooms.Add(currentRoom);

            // spawn the hallway to the right of the room, if it's not the last column
            if (col < columns - 1)
            {
                Vector3 hallwayPos = roomPos + new Vector3(roomSpacing / 2f + hallwayXOffsetEW, 0 + hallwayYOffsetEW, 0 + hallwayZOffset);
                Quaternion hallwayRotation = Quaternion.identity;
                GameObject hallway = Instantiate(hallwayPrefab, hallwayPos, hallwayRotation);
                // hallway.transform.localScale = new Vector3(roomSpacing, 1f, 1f);
            }

            // spawn the hallway below the room, if it's not the last row
            if (row < rows - 1)
            {
                Vector3 hallwayPos = roomPos + new Vector3(0 + hallwayXOffset, 0 + hallwayYOffset, roomSpacing / 2f + hallwayZOffsetNS);
                Quaternion hallwayRotation = Quaternion.Euler(0f, 90f, 0f);
                GameObject hallway = Instantiate(hallwayPrefab, hallwayPos, hallwayRotation);
                // hallway.transform.localScale = new Vector3(roomSpacing, 1f, 1f);
            }
        }
    }
}
}


// using UnityEngine;
// using System.Collections.Generic;

// public class RoomSpawner : MonoBehaviour
// {
//     public GameObject roomPrefab; // prefab of the room to spawn
//     public GameObject hallwayPrefab; // prefab of the hallway to spawn
//     public int rows = 3; // number of rows of rooms
//     public int columns = 3; // number of columns of rooms
//     public float roomSpacing = 10f; // distance between each room
//     public float hallwayLength = 10f; // length of each hallway

//     private List<GameObject> spawnedRooms = new List<GameObject>(); // list to store spawned rooms

//     void Start()
//     {
//         // spawn the rooms
//         for (int row = 0; row < rows; row++)
//         {
//             for (int col = 0; col < columns; col++)
//             {
//                 Vector3 roomPos = new Vector3(col * roomSpacing, 0, row * roomSpacing);
//                 GameObject currentRoom = Instantiate(roomPrefab, roomPos, Quaternion.identity);
//                 spawnedRooms.Add(currentRoom);

//                 // spawn the hallway to the right of the room, if it's not the last column
//                 if (col < columns - 1)
//                 {
//                     Vector3 hallwayPos = roomPos + new Vector3(roomSpacing / 2f, 0, 0);
//                     Quaternion hallwayRotation = Quaternion.identity;
//                     GameObject hallway = Instantiate(hallwayPrefab, hallwayPos, hallwayRotation);
//                     hallway.transform.localScale = new Vector3(roomSpacing, 1f, 1f);
//                 }

//                 // spawn the hallway below the room, if it's not the last row
//                 if (row < rows - 1)
//                 {
//                     Vector3 hallwayPos = roomPos + new Vector3(0, 0, roomSpacing / 2f);
//                     Quaternion hallwayRotation = Quaternion.Euler(0f, 90f, 0f);
//                     GameObject hallway = Instantiate(hallwayPrefab, hallwayPos, hallwayRotation);
//                     hallway.transform.localScale = new Vector3(roomSpacing, 1f, 1f);
//                 }
//             }
//         }
//     }
// }

// using UnityEngine;
// using System.Collections.Generic;

// public class RoomSpawner : MonoBehaviour
// {
//     public GameObject roomPrefab; // prefab of the room to spawn
//     public GameObject hallwayPrefab; // prefab of the hallway to spawn
//     public int rows = 3; // number of rows of rooms
//     public int columns = 3; // number of columns of rooms
//     public float roomSpacing = 10f; // distance between each room
//     public float hallwayLength = 10f; // length of each hallway
//     public float hallwayXOffset = 0f; // x offset for the hallway
//     public float hallwayYOffset = 0f; // y offset for the hallway
//     public float hallwayZOffset = 0f; // z offset for the hallway

//     private List<GameObject> spawnedRooms = new List<GameObject>(); // list to store spawned rooms

//     void Start()
//     {
//         // spawn the rooms
//         for (int row = 0; row < rows; row++)
//         {
//             for (int col = 0; col < columns; col++)
//             {
//                 Vector3 roomPos = new Vector3(col * roomSpacing, 0, row * roomSpacing);
//                 GameObject currentRoom = Instantiate(roomPrefab, roomPos, Quaternion.identity);
//                 spawnedRooms.Add(currentRoom);

//                 // spawn the hallway to the right of the room, if it's not the last column
//                 if (col < columns - 1)
//                 {
//                     Vector3 hallwayPos = roomPos + new Vector3(roomSpacing / 2f + hallwayXOffset, 0 + hallwayYOffset, 0 + hallwayZOffset);
//                     Quaternion hallwayRotation = Quaternion.identity;
//                     GameObject hallway = Instantiate(hallwayPrefab, hallwayPos, hallwayRotation);
//                     hallway.transform.localScale = new Vector3(roomSpacing, 1f, 1f);
//                 }

//                 // spawn the hallway below the room, if it's not the last row
//                 if (row < rows - 1)
//                 {
//                     Vector3 hallwayPos = roomPos + new Vector3(0 + hallwayXOffset, 0 + hallwayYOffset, roomSpacing / 2f + hallwayZOffset);
//                     Quaternion hallwayRotation = Quaternion.Euler(0f, 90f, 0f);
//                     GameObject hallway = Instantiate(hallwayPrefab, hallwayPos, hallwayRotation);
//                     hallway.transform.localScale = new Vector3(roomSpacing, 1f, 1f);
//                 }
//             }
//         }
//     }
// }


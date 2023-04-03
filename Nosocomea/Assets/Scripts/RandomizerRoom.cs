using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizerRoom : MonoBehaviour
{
    public float offsetX = 0f; // X offset
    public float offsetY = 0f; // Y offset
    public float offsetZ = 0f; // Z offset

    void Start()
    {
        // Choose a random prefab from the array
        int randomIndex = Random.Range(0, roomPrefabs.Length);
        GameObject roomPrefab = roomPrefabs[randomIndex];

        // Get the position of the game object this script is attached to
        Vector3 position = transform.position;

        // Add the specified offsets to the position
        position += new Vector3(offsetX, offsetY, offsetZ);

        // Instantiate the prefab at the modified position
        Instantiate(roomPrefab, position, Quaternion.identity);
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RandomizerRoom : MonoBehaviour
// {
//     public GameObject[] roomPrefabs; // Array of room prefabs to choose from
//     public float offsetX = 0f; // X offset
//     public float offsetY = 0f; // Y offset
//     public float offsetZ = 0f; // Z offset

//     void Start()
//     {
//         // Choose a random prefab from the array
//         int randomIndex = Random.Range(0, roomPrefabs.Length);
//         GameObject roomPrefab = roomPrefabs[randomIndex];

//         // Instantiate the prefab at its geometric origin with the specified offsets
//         Vector3 position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z + offsetZ);
//         Instantiate(roomPrefab, position, Quaternion.identity);
//     }
// }

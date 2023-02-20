using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabGenerator : MonoBehaviour
{
    public List<GameObject> prefabList;
    public float offsetMagnitude = 1.0f;

    void OnDestroy()
    {
        // Generate a random index for the prefab list
        int index = Random.Range(0, prefabList.Count);

        // Calculate a random offset vector
        Vector3 offset = Random.insideUnitSphere * offsetMagnitude;

        // Instantiate the selected prefab with the calculated offset
        Instantiate(prefabList[index], transform.position + offset, Quaternion.identity);
    }
}

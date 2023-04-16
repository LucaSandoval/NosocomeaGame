using UnityEngine;

public class PrefabInstantiator : MonoBehaviour
{
    public GameObject prefab;
    public float instantiationInterval = 2f; // set the time between each instantiation here

    void Start()
    {
        InvokeRepeating("InstantiatePrefab", 0f, instantiationInterval);
    }

    void InstantiatePrefab()
    {
        Vector3 randomRotation = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        GameObject newSpawn = Instantiate(prefab, transform.position, Quaternion.Euler(randomRotation));
        newSpawn.transform.SetParent(transform);
    }
}

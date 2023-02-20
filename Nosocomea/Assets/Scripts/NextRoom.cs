using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoom : MonoBehaviour
{
    public GameObject objectToDestroy;
    public GameObject prefabToInstantiate;

    void Update()
    {
        if (objectToDestroy == null)
        {
            Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
            Destroy(this);
        }
    }
}


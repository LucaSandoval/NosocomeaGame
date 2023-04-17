using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoSpawner : MonoBehaviour
{
    public GameObject[] decoPossibilities;

    public void Awake()
    {
        int rand = Random.Range(0, decoPossibilities.Length);
        GameObject newDeco = Instantiate(decoPossibilities[rand]);
        newDeco.transform.position = transform.position;
        newDeco.transform.SetParent(transform);
    }
}

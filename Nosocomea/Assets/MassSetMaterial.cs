using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassSetMaterial : MonoBehaviour
{
    private MeshRenderer[] children;

    public void Awake()
    {
        children = GetComponentsInChildren<MeshRenderer>();
    }

    public void SetMassMaterial(Material mat)
    {
        for(int i = 0; i < children.Length; i++)
        {
            children[i].material = mat;
        }
    }

    //public void Recurse(Material mat, MeshRenderer[] children, int depth = 0, int maxDepth = 10)
    //{
    //    if (depth >= maxDepth || children.Length == 0)
    //    {
    //        return;
    //    }

    //    for (int i = 0; i < children.Length; i++)
    //    {
    //        children[i].material = mat;

    //        MeshRenderer[] thisChild = children[i].GetComponentsInChildren<MeshRenderer>();
    //        if (thisChild.Length > 0)
    //        {
    //            Recurse(mat, thisChild, depth + 1, maxDepth);
    //        }
    //    }
    //}
}

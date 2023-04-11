using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    private MeshRenderer ren;
    public Material[] frames;
    private int frameID;


    [HideInInspector]
    public float attackLength;
    private float frameRate;
    private float timer;

    private void Start()
    {
        ren = GetComponent<MeshRenderer>();
        frameRate = attackLength / frames.Length;
        timer = frameRate;
    }

    void FixedUpdate()
    {
        if (frameID < frames.Length - 1)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            } else
            {
                frameID++;
                timer = frameRate;
            }
        } else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    private void Update()
    {
        ren.material = frames[frameID];
    }
}

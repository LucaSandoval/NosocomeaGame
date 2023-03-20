using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAgent))]
public class EnemyLocomotion : MonoBehaviour
{
  private EnemyAgent agent;

  // Start is called before the first frame update
  void Start()
  {
    agent = GetComponent<EnemyAgent>();
  }

  // Update is called once per frame
  void Update()
  {
    if (agent.ShouldMove())
    {
      agent.Move();
    }
  }
}

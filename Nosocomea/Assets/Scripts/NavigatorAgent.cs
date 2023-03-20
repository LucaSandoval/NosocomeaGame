using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigatorAgent : MonoBehaviour
{
  private Transform target;
  private NavMeshAgent agent;

  // Start is called before the first frame update
  void Start()
  {
    if (target == null)
    {
      target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    agent = GetComponent<NavMeshAgent>();
  }

  public void RecalculatePath()
  {
    agent.SetDestination(target.position);
  }

  public void SetTarget(Transform newTarget)
  {
    target = newTarget;
  }
}

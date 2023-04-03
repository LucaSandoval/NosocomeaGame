using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator _animator;
    int blend;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        blend = Animator.StringToHash("Blend");
    }
    public void UpdateAnimatorValues(float blendMovement)
    {
        _animator.SetFloat(blend, blendMovement, 0.1f, Time.deltaTime);
    }
}

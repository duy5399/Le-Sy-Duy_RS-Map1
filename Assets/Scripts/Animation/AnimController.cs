using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MyBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected AnimState animState;


    protected virtual void Awake()
    {
        animator = this.transform.GetComponentInChildren<Animator>();
        animState = AnimState.Idle;
    }

    public virtual void TriggerAnim(string animName, float animSpeed = 1f, bool force = false)
    {
        
    }

    public void ResetAllTriggers(Animator animator)
    {
        foreach (var trigger in animator.parameters)
        {
            if (trigger.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(trigger.name);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimation : AnimController
{
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {

    }

    public override void TriggerAnim(string animName, float animSpeed = 1f, bool force = false)
    {
        if (!animator || animName == "Cat_Idle" && this.animState == AnimState.Idle || animName == "Cat_Walk" && this.animState == AnimState.Walk)
        {
            return;
        }
        if (animName == "Cat_Idle")
        {
            animState = AnimState.Idle;
        }
        else if (animName == "Cat_Walk")
        {
            animState = AnimState.Walk;
        }

        ResetAllTriggers(animator);
        animator.speed = animSpeed;

        if (force)
        {
            animator.Play(animName);
        }
        else
        {
            animator.SetTrigger(animName);
        }
        Debug.Log("TriggerAnim: " + animName);
    }

    public void TriggerIdle(bool force = false)
    {
        TriggerAnim("Cat_Idle", 1f, force);
    }

    public void TriggerWalk(bool force = false)
    {
        TriggerAnim("Cat_Walk", 1f, force);
    }
}

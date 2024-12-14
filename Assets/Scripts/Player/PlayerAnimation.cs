using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAnimation : AnimController
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
        if (!animator || animName == "Character_Idle" && this.animState == AnimState.Idle || animName == "Character_Walk" && this.animState == AnimState.Walk || animName == "Character_FastRun" && this.animState == AnimState.Run)
        {
            return;
        }
        if (animName == "Character_Idle")
        {
            animState = AnimState.Idle;
        }
        if (animName == "Character_Walk")
        {
            animState = AnimState.Walk;
        }
        else if (animName == "Character_FastRun")
        {
            animState = AnimState.Run;
        }
        else if (animName == "Character_LookAround")
        {
            animState = AnimState.LookAround;
        }
        else if (animName == "Character_Tired")
        {
            animState = AnimState.LookAround;
        }
        else if (animName == "Character_Waving")
        {
            animState = AnimState.Waving;
        }
        else
        {
            animState = AnimState.Other;
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
    }

    public void TriggerIdle(bool force = false)
    {
        TriggerAnim("Character_Idle", 1f, force);
    }

    public void TriggerWalk(bool force = false)
    {
        TriggerAnim("Character_Walk", 1f, force);
    }

    public void TriggerRun(bool force = false)
    {
        TriggerAnim("Character_FastRun", 1f, force);
    }

    public void TriggerLookAround(bool force = false)
    {
        TriggerAnim("Character_LookAround", 1f, force);
    }

    public void TriggerTired(bool force = false)
    {
        TriggerAnim("Character_Tired", 1f, force);
    }

    public void TriggerWaving(bool force = false)
    {
        TriggerAnim("Character_Waving", 1f, force);
    }
}

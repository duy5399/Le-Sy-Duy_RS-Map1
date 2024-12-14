using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameObjectTag
{
    TriggerNewStreet = 0,
    Animal = 1,
    Player = 2,
    Tsunami = 3,
    FinishPhase1 = 4,
    FinishPhase2 = 5,
}

public enum SceneName
{
    MainScene = 0,
    LevelScene = 1
}

public enum AnimState
{
    Idle = 0,
    LookAround = 1,
    Walk = 2,
    Run = 3,
    Tired = 4,
    Waving = 5,
    Other = 6
}
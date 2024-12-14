using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : Singleton<SettingsController>
{
    protected void Awake()
    {
        base.Awake();
        ChildActive(false);
    }
}

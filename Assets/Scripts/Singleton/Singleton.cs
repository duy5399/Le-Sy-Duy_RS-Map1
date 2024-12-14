using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Singleton<T> : MyBehaviour where T : Singleton<T>
{
    public static T instance;
    [SerializeField] protected List<Transform> childs = new List<Transform>();

    protected void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this as T;
        }

        for (int i = 0; i < this.transform.childCount; ++i)
        {
            childs.Add(this.transform.GetChild(i));
        }
    }

    public virtual void ChildActive(bool isActive)
    {
        foreach (Transform child in childs)
        {
            child.gameObject.SetActive(isActive);
        }
    }
}

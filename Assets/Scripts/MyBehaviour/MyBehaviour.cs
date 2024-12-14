using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBehaviour : MonoBehaviour
{
    public void WaitFor(float delay, Action func)
    {
        if (func == null)
        {
            return;
        }
        if (delay <= 0.0001f)
        {
            func();
        }
        else
        {
            Coroutine coroutine = StartCoroutine(_WaitFor(delay, func));
        }
    }

    public IEnumerator _WaitFor(float delay, Action func)
    {
        yield return new WaitForSeconds(delay);
        func();
    }
}

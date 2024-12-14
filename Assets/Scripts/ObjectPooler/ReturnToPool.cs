using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    public GameObject objKey;

    private void OnDisable()
    {
        ObjectPooler.instance.Release(objKey, this.gameObject);
    }
}

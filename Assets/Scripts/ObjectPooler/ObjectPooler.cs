using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    private SerializedDictionary<GameObject, Queue<GameObject>> poolDict;
    private GameObject obj;
    private ReturnToPool returnToPool;

    protected void Awake()
    {
        base.Awake();
        poolDict = new SerializedDictionary<GameObject, Queue<GameObject>>();
    }

    public GameObject Get(GameObject objKey)
    {
        if (!poolDict.ContainsKey(objKey) || poolDict[objKey].Count <= 0)
        {
            return NewEnqueue(objKey);
        }
        obj = poolDict[objKey].Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void Release(GameObject objKey, GameObject objValue)
    {
        if (!poolDict.ContainsKey(objKey))
        {
            Debug.Log("poolDict không chứa key: " + objKey.name);
            return;
        }
        poolDict[objKey].Enqueue(objValue);
    }

    public void Clear(GameObject objKey)
    {
        if (!poolDict.ContainsKey(objKey))
        {
            Debug.Log("poolDict không chứa key: " + objKey.name);
            return;
        }
        poolDict[objKey].Clear();
    }

    GameObject NewEnqueue(GameObject gameObject)
    {
        obj = GameObject.Instantiate(gameObject);
        returnToPool = obj.AddComponent<ReturnToPool>();
        returnToPool.objKey = gameObject;
        poolDict[gameObject].Enqueue(obj);
        return obj;
    }
}

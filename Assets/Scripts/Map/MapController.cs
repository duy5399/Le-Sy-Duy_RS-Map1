using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MapController : Singleton<MapController>
{
    [SerializeField] private List<GameObject> roadObjects;
    [SerializeField] private float spaceRoadSections;

    protected void Awake()
    {
        base.Awake();
        roadObjects = new List<GameObject>();
    }

    private void Start()
    {
        
    }

    //Kéo dài đường đi của map
    public void ChangeToNewRoad(GameObject currRoad)
    {
        if (!currRoad)
        {
            return;
        }
        //Kiểm tra từng road của từng roadsection
        //Nếu không phải currRoad và vị trí nhỏ hơn currRoad thì đẩy lên đầu currRoad để kéo dài đường đi
        for (int i = 0; i < roadObjects.Count; i++)
        {
            if (roadObjects[i] == currRoad || roadObjects[i].transform.position.x > currRoad.transform.position.x)
            {
                continue;
            }
            roadObjects[i].transform.position = new Vector3(currRoad.transform.position.x, currRoad.transform.position.y, currRoad.transform.position.z + spaceRoadSections);
            break;
        }
    }
}

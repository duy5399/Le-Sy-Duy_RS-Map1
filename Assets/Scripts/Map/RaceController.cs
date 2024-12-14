using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[Serializable]
public class RaceController : Singleton<RaceController>
{
    [SerializeField] private int numberOfCats;
    [SerializeField] private int numberOfCars;
    [SerializeField] private int numberOfTrees;
    [SerializeField] private int numberOfHouses;

    [SerializeField] private GameObject roadObject;
    [SerializeField] private List<GameObject> sandObjects;

    public List<AssetReference> assetReferences;
    public List<AssetReference> catAssetReferences;
    public List<AssetReference> carAssetReferences;

    [SerializeField] private List<GameObject> objectsOnRoad;

    [SerializeField] private float catSpawnMinRange;
    [SerializeField] private float carSpawnMinRange;
    [SerializeField] private Dictionary<AssetReference, AsyncOperationHandle> dictAsyncOperationHandle;
    public bool isDone { get; private set; }

    protected void Awake()
    {
        base.Awake();
        dictAsyncOperationHandle = new Dictionary<AssetReference, AsyncOperationHandle>();
    }

    private void Start()
    {
        StartCoroutine(OnAddressablesCompleted());
        //Addressables.InitializeAsync().Completed += (x) =>
        //{
        //    StartCoroutine(OnAddressablesCompleted());
        //};
    }

    //Tải trước Addressables
    public IEnumerator OnAddressablesCompleted()
    {
        bool isDone = false;
        for (int i = 0; i < assetReferences.Count; i++)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(assetReferences[i]);
            yield return handle;
            if(handle.Status == AsyncOperationStatus.Succeeded)
            {
                if (!dictAsyncOperationHandle.ContainsKey(assetReferences[i]))
                {
                    dictAsyncOperationHandle.Add(assetReferences[i], handle);
                }
                if (i == assetReferences.Count - 1)
                {
                    SpawnTreeAndHouse(numberOfTrees + numberOfHouses);
                }
            }
        }
        for (int i = 0; i < catAssetReferences.Count; i++)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(catAssetReferences[i]);
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                if (!dictAsyncOperationHandle.ContainsKey(catAssetReferences[i]))
                {
                    dictAsyncOperationHandle.Add(catAssetReferences[i], handle);
                }
                if (i == catAssetReferences.Count - 1)
                {
                    Debug.Log(LevelController.instance.numberOfCats);
                    SpawnCats(LevelController.instance.numberOfCats);
                }
            }
        }
        for (int i = 0; i < carAssetReferences.Count; i++)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(carAssetReferences[i]);
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                if (!dictAsyncOperationHandle.ContainsKey(carAssetReferences[i]))
                {
                    dictAsyncOperationHandle.Add(carAssetReferences[i], handle);
                }
                if (i == carAssetReferences.Count - 1)
                {
                    Debug.Log(LevelController.instance.numberOfCars);
                    SpawnCars(LevelController.instance.numberOfCars);
                    isDone = true;
                }
            }
        }
        while (!isDone)
        {
            yield return null;
        }
        this.isDone = true;
    }

    void SpawnTreeAndHouse(int number)
    {
        for(int i = 0; i < sandObjects.Count; i++)
        {
            SpawnRandomPosition(sandObjects[i], numberOfHouses, 3, 0, assetReferences);
        }
    }

    public void SpawnCats(int number)
    {
        SpawnRandomPosition(roadObject, number, 5, catSpawnMinRange, catAssetReferences);
    }

    public void SpawnCars(int number)
    {
        SpawnRandomPosition(roadObject, number, 5, carSpawnMinRange, carAssetReferences, true);
    }

    GameObject GetRandomAsset(List<AssetReference> assetReferences)
    {
        AssetReference asset = assetReferences[UnityEngine.Random.Range(0, assetReferences.Count)];
        return (GameObject)dictAsyncOperationHandle[asset].Result;
    }

    void SpawnRandomPosition(GameObject plane, int itemQuantity, int searchTime, float minRange, List<AssetReference> assetReferences, bool isRotate = false)
    {
        GameObject obj;
        for (var i = 0; i < itemQuantity; i++)
        {
            //Giới hạn số lượt tìm kiếm vị trí ngẫu nhiên
            var searchCount = searchTime;
            while (searchCount > 0)
            {
                searchCount--;
                //Chọn vị trí ngẫu nhiên
                float minX = plane.transform.position.x - (plane.transform.localScale.x * 10 / 2);
                float maxX = plane.transform.position.x + (plane.transform.localScale.x * 10 / 2);
                float minZ = plane.transform.position.z - (plane.transform.localScale.z * 10 / 2);
                float maxZ = plane.transform.position.z + (plane.transform.localScale.z * 10 / 2);
                Vector3 position = GetRandomPosition(minX, maxX, (maxZ - minZ) / itemQuantity * i + minZ, (maxZ - minZ) / itemQuantity * (i + 1) + minZ);
                //Kiểm tra xem có trùng với object nào khác không
                //Nếu không thì spawn object với vị trí vừa tìm được;
                if (IsOverlapOnOtherObject(minRange, position, objectsOnRoad))
                {
                    obj = GameObject.Instantiate(GetRandomAsset(assetReferences));
                    obj.transform.position = position;
                    if (isRotate)
                    {
                        obj.transform.rotation = GetRandomRotation(0, 360);
                    }
                    objectsOnRoad.Add(obj);
                    break;
                }
            }
        }
    }

    bool IsOverlapOnOtherObject(float minRange, Vector3 position, List<GameObject> objects)
    {
        foreach (var obj in objects)
        {
            if (Vector3.Distance(position, obj.transform.position) < minRange)
                return false;
        }
        return true;
    }

    Vector3 GetRandomPosition(float minX, float maxX, float minZ, float maxZ)
    {
        Vector3 position = Vector3.zero;
        position.x = UnityEngine.Random.Range(minX, maxX);
        position.z = UnityEngine.Random.Range(minZ, maxZ);
        return position;
    }

    Quaternion GetRandomRotation(float minY, float maxY)
    {
        return Quaternion.Euler(0, UnityEngine.Random.Range(minY, maxY), 0);
    }


    protected void OnDestroy()
    {
        foreach (var handle in dictAsyncOperationHandle)
        {
            Addressables.Release(handle.Value);
        }
    }
}
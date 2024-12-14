using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class LevelController : Singleton<LevelController>, ISaveable
{
    [SerializeField] private int _raceLevel;
    public int numberOfCats { get; private set; }
    public int numberOfCars { get; private set; }
    public float weightOfCat { get; private set; }
    public float speedOfTsunami { get; private set; }

    public int raceLevel { 
        get { return _raceLevel; }
        set { _raceLevel = value; }
    }

    protected void Awake()
    {
        base.Awake();
        Load();
        SetLevelMap(raceLevel);
    }

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey("raceLevel"))
        {
            PlayerPrefs.SetInt("raceLevel", 1);
            return;
        }
        raceLevel = PlayerPrefs.GetInt("raceLevel");
    }

    public void Save()
    {
        PlayerPrefs.SetInt("raceLevel", raceLevel);
    }

    public void SetLevelMap(int level)
    {
        //cat = 5 + level;
        //tsunami's speed = 40
        numberOfCats = 5;
        numberOfCars = 10;
        weightOfCat = 0.5f * level;
        speedOfTsunami = 10f + level;
    }

    public float IncreaseSpeedOfTsunami()
    {
        speedOfTsunami = 40f + raceLevel;
        return speedOfTsunami;
    }

    public float GetReward(float distance, int cats)
    {
        return distance * raceLevel * cats;
    }

    private void OnDestroy()
    {
        Save();
    }
}

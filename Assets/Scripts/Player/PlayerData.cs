using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class StatData
{
    public float stamina;
    public float speed;
    public float income;
    public float money;

    public StatData()
    {
        stamina = 10;
        speed = 1;
        income = 1;
        money = 100;
    }
}

public class PlayerData : MonoBehaviour, ISaveable
{
    [SerializeField] private StatData statData;
    [SerializeField] private float _completedTime;
    [SerializeField] private float _currSpeed;
    [SerializeField] private float _highestSpeed;
    [SerializeField] private float _highestDistance;
    [SerializeField] private float boostSpeed;
    [SerializeField] private float boostTime;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private List<GameObject> _catsRescued;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Coroutine coroutineBoostSpeed;

    public float stamina
    {
        get { return statData.stamina; }
        set { statData.stamina = value; }
    }

    public float speed
    {
        get { return statData.speed; }
        set
        {
            if (value <= 0)
            {
                statData.speed = 0.1f;
            }
            else
            {
                statData.speed = value;
            }
        }
    }

    public float income
    {
        get { return statData.income; }
        set { statData.income = value; }
    }

    public float money
    {
        get { return statData.money; }
        set { statData.money = value; }
    }


    public List<GameObject> catsRescued
    {
        get { return _catsRescued; }
        set { _catsRescued = value; }
    }

    public float completedTime
    {
        get { return _completedTime; }
        private set { _completedTime = value; }
    }
    public float highestSpeed
    {
        get { return _highestSpeed; }
        private set { _highestSpeed = value; }
    }

    public float highestDistance
    {
        get { return _highestDistance; }
        private set { _highestDistance = value; }
    }

    public float currSpeed
    {
        get { return _currSpeed; }
        set {
            if (value <= 0)
            {
                _currSpeed = 1f;
            }
            else if(value > speed + (stamina / speed))
            {
                _currSpeed = speed + (stamina / speed);
            }
            else
            {
                _currSpeed = value;
            }
        }
    }

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerMovement = this.GetComponent<PlayerMovement>();
        Load();
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey("statData"))
        {
            statData = new StatData();
        }
        else
        {
            statData = JsonConvert.DeserializeObject<StatData>(PlayerPrefs.GetString("statData"));
        }
        currSpeed = statData.speed;
    }

    public void Save()
    {
        PlayerPrefs.SetString("statData", JsonConvert.SerializeObject(statData));
    }

    private void Update()
    {
        float currDistance = this.transform.position.z / 3.333333333333333f;
        if(currDistance > highestDistance)
        {
            highestDistance = currDistance;
        }
        if (rb.velocity != Vector3.zero && currSpeed > highestSpeed)
        {
            highestSpeed = currSpeed;
        }
        if (playerMovement.moveWithJoystick)
        {
            completedTime += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {

    }

    public void BoostSpeed(float boostSpeed)
    {
        try
        {
            StopCoroutine(coroutineBoostSpeed);
        }
        catch
        {
            Debug.Log("Không có coroutineBoostSpeed");
        }
        coroutineBoostSpeed = StartCoroutine(_BoostSpeed(boostSpeed));
    }

    IEnumerator _BoostSpeed(float boostSpeed)
    {
        this.boostSpeed += (boostSpeed * 0.1f)  ;
        currSpeed += (boostSpeed * 0.1f);
        SpeedMeterController.instance.maxSpeed = currSpeed;
        SpeedMeterController.instance.DisplayMaxSpeed((float)Math.Round(currSpeed, 2));
        boostTime = 5f;
        while(boostTime > 0)
        {
            boostTime -= Time.fixedDeltaTime;
            yield return null;
        }
        currSpeed -= this.boostSpeed;
        this.boostSpeed = 0;
        SpeedMeterController.instance.maxSpeed = currSpeed;
        SpeedMeterController.instance.DisplayMaxSpeed((float)Math.Round(currSpeed, 2));
    }

    private void OnDestroy()
    {
        Save();
    }
}
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class SpeedMeterController : Singleton<SpeedMeterController>
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Image imgNeedle;
    [SerializeField] private TextMeshProUGUI txtMaxSpeed;

    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;

    public float speed
    {
        get { return _speed; }
        set 
        { 
            if(value > maxSpeed)
            {
                _speed = maxSpeed;
            }
            else if(value < 0)
            {
                _speed = 0;
            }
            else
            {
                _speed = value;
            }
        }
    }
    public float maxSpeed
    {
        get { return _maxSpeed; }
        set { _maxSpeed = value; }
    }

    protected void Awake()
    {
        base.Awake();
        imgNeedle = this.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        txtMaxSpeed = this.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        Load();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (speed > 0)
        {
            imgNeedle.transform.eulerAngles = new Vector3(0, 0, speed / maxSpeed * 180);
            speed -= Time.fixedDeltaTime;
        }
    }

    void Load()
    {
        maxSpeed = playerData.speed;
        DisplayMaxSpeed((float)Math.Round(maxSpeed, 2));
    }

    public void DisplayMaxSpeed(float amount = 0f)
    {
        txtMaxSpeed.text = amount.ToString();
    }
}

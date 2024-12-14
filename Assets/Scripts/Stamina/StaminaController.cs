using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : Singleton<StaminaController>
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private float _currStamina;
    [SerializeField] private float _maxStamina;
    [SerializeField] private bool isRegen;
    [SerializeField] private float staminaPerSecond;
    [SerializeField] private float autoRegenTime;

    [SerializeField] private Slider sliderStamina;
    [SerializeField] private TextMeshProUGUI txtStamina;

    public float currStamina
    {
        get { return _currStamina; }
        set { _currStamina = value; }
    }

    public float maxStamina
    {
        get { return _maxStamina; }
        set { _maxStamina = value; }
    }

    protected void Awake()
    {
        base.Awake();
        isRegen = false;
        staminaPerSecond = 10f;
        autoRegenTime = -1f;
        sliderStamina = this.transform.GetChild(0).GetComponent<Slider>();
        txtStamina = sliderStamina.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        currStamina = playerData.stamina;
        maxStamina = playerData.stamina;
        DisplayStamina(currStamina, maxStamina);
    }

    private void Update()
    {
        //Click vào màn hình sẽ sử dụng năng lượng
        //if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        //{
        //    //Touch touch = Input.GetTouch(0);
        //    UseStamina();
        //}
    }

    private void FixedUpdate()
    {
        //Khi thời gian reset > 0, tự động đếm ngược
        //Khi đếm ngược hoàn tất, kích hoạt việc hồi phục năng lượng
        if(autoRegenTime > 0f && !isRegen)
        {
            autoRegenTime -= Time.fixedDeltaTime;
        }
        else
        {
            autoRegenTime = -1f;
            isRegen = true;
        }
        //Hồi phục năng lượng
        if (isRegen)
        {
            currStamina += staminaPerSecond / 50;
            if (currStamina >= maxStamina)
            {
                isRegen = false;
                sliderStamina.fillRect.GetComponent<Image>().color = new Color32(221, 221, 221, 255);
                currStamina = maxStamina;
            }
        }
        DisplayStamina(currStamina, maxStamina);
    }

    public void Save()
    {
        
    }

    public bool UseStamina(float stamina)
    {
        //Không thể sử dụng nếu đang trong quá trình hồi phục năng lượng
        if (isRegen)
        {
            return false;
        }
        //Mỗi lần sử dụng năng lượng, làm mới lại thời gian đếm ngược
        //Năng lượng tiêu hao dựa trên Tốc độ nhân vật
        //Khi năng lượng về 0, kích hoạt việc hồi phục
        autoRegenTime = 2f;     
        currStamina -= stamina;
        if(currStamina <= 0)
        {
            isRegen = true;
            sliderStamina.fillRect.GetComponent<Image>().color = new Color32(37, 37, 37, 255);
            currStamina = 0;
        }
        return true;
    }

    //Hiển thị thông tin năng lượng
    public void DisplayStamina(float curr = 0f, float max = 0f)
    {
        sliderStamina.value = curr / max;
        txtStamina.text = ((int)curr).ToString() + "/" + ((int)max).ToString();
    }
}

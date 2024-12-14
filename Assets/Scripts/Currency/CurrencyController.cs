using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class CurrencyController : Singleton<CurrencyController>, ISaveable
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextMeshProUGUI txtAmount;

    protected void Awake()
    {
        base.Awake();
        txtAmount = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        Load();
    }

    void Update()
    {

    }

    public void Load()
    {
        DisplayCurrency(playerData.money);
    }

    public void Save()
    {
        
    }

    public void AddMoney(float money)
    {
        playerData.money += money;
    }

    public void DisplayCurrency(float amount = 0f)
    {
        txtAmount.text = Math.Round(amount, 2).ToString() + "$";
    }

    private void OnDestroy()
    {
        Save();
    }
}

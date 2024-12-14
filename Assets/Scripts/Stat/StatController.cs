using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

[Serializable]
public class UpgradeStat
{
    public float upgradeStaminaRequired;
    public float upgradeSpeedRequired;
    public float upgradeIncomeRequired;

    public UpgradeStat()
    {
        upgradeStaminaRequired = 1;
        upgradeSpeedRequired = 1;
        upgradeIncomeRequired = 1;
    }
}

public class StatController : Singleton<StatController>, ISaveable
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private UpgradeStat upgradeStat;

    [SerializeField] private Button btnUpgradeStamina;
    [SerializeField] private Button btnUpgradeSpeed;
    [SerializeField] private Button btnUpgradeIncome;

    [SerializeField] private TextMeshProUGUI txtCurrStamina;
    [SerializeField] private TextMeshProUGUI txtCurrSpeed;
    [SerializeField] private TextMeshProUGUI txtCurrIncome;

    [SerializeField] private TextMeshProUGUI txtUpgradeStaminaRequired;
    [SerializeField] private TextMeshProUGUI txtUpgradeSpeedRequired;
    [SerializeField] private TextMeshProUGUI txtUpgradeIncomeRequired;

    public float upgradeStaminaRequired
    {
        get { return upgradeStat.upgradeStaminaRequired; }
        set { upgradeStat.upgradeStaminaRequired = value; }
    }

    public float upgradeSpeedRequired
    {
        get { return upgradeStat.upgradeSpeedRequired; }
        set { upgradeStat.upgradeSpeedRequired = value; }
    }

    public float upgradeIncomeRequired
    {
        get { return upgradeStat.upgradeIncomeRequired; }
        set { upgradeStat.upgradeIncomeRequired = value; }
    }

    protected void Awake()
    {
        base.Awake();
        btnUpgradeStamina = this.transform.GetChild(1).GetComponent<Button>();
        btnUpgradeSpeed = this.transform.GetChild(2).GetComponent<Button>();
        btnUpgradeIncome = this.transform.GetChild(3).GetComponent<Button>();

        txtCurrStamina = btnUpgradeStamina.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        txtCurrSpeed = btnUpgradeSpeed.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        txtCurrIncome = btnUpgradeIncome.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        txtUpgradeStaminaRequired = btnUpgradeStamina.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        txtUpgradeSpeedRequired = btnUpgradeSpeed.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        txtUpgradeIncomeRequired = btnUpgradeIncome.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        btnUpgradeStamina.onClick.AddListener(OnClick_UpgradeStamina);
        btnUpgradeSpeed.onClick.AddListener(OnClick_UpgradeSpeed);
        btnUpgradeIncome.onClick.AddListener(OnClick_UpgradeIncome);
    }

    private void OnDisable()
    {
        btnUpgradeStamina.onClick.RemoveAllListeners();
        btnUpgradeSpeed.onClick.RemoveAllListeners();
        btnUpgradeIncome.onClick.RemoveAllListeners();
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
        if (!PlayerPrefs.HasKey("upgradeStat"))
        {
            upgradeStat = new UpgradeStat();
        }
        else
        {
            upgradeStat = JsonConvert.DeserializeObject<UpgradeStat>(PlayerPrefs.GetString("upgradeStat"));
        }

        txtCurrStamina.text = Math.Round(playerData.stamina, 2).ToString();
        txtCurrSpeed.text = Math.Round(playerData.speed, 2).ToString();
        txtCurrIncome.text = Math.Round(playerData.income, 2).ToString();

        txtUpgradeStaminaRequired.text = Math.Round(upgradeStaminaRequired, 2).ToString();
        txtUpgradeSpeedRequired.text = Math.Round(upgradeSpeedRequired, 2).ToString();
        txtUpgradeIncomeRequired.text = Math.Round(upgradeIncomeRequired, 2).ToString();
    }

    public void Save()
    {
        PlayerPrefs.SetString("upgradeStat", JsonConvert.SerializeObject(upgradeStat));
    }

    void OnClick_UpgradeStamina()
    {
        if(playerData.money < upgradeStaminaRequired)
        {
            return;
        }

        playerData.money -= upgradeStaminaRequired;
        CurrencyController.instance.DisplayCurrency(playerData.money);

        //Trung bình giá trị mới tăng theo level đã nâng cấp
        int averageStamina = (int)(playerData.stamina / 15);
        playerData.stamina += UnityEngine.Random.Range(averageStamina, averageStamina + 4);
        txtCurrStamina.text = playerData.stamina.ToString();

        StaminaController.instance.maxStamina = playerData.stamina;
        StaminaController.instance.DisplayStamina(playerData.stamina, playerData.stamina);

        int averageUpgradeStaminaRequired = (int)(upgradeStaminaRequired / 9);
        upgradeStaminaRequired = upgradeStaminaRequired + UnityEngine.Random.Range(averageUpgradeStaminaRequired, averageUpgradeStaminaRequired + 2);
        txtUpgradeStaminaRequired.text = upgradeStaminaRequired.ToString();
    }

    void OnClick_UpgradeSpeed()
    {
        if (playerData.money < upgradeSpeedRequired)
        {
            return;
        }
        playerData.money -= upgradeSpeedRequired;
        CurrencyController.instance.DisplayCurrency(playerData.money);

        //Trung bình giá trị mới tăng theo level đã nâng cấp
        float averageSpeed = playerData.speed / 30;
        playerData.speed += averageSpeed;
        txtCurrSpeed.text = Math.Round(playerData.speed, 2).ToString();

        SpeedMeterController.instance.maxSpeed = playerData.speed;
        SpeedMeterController.instance.DisplayMaxSpeed((float)Math.Round(playerData.speed, 2));

        float averageUpgradeSpeedRequired = upgradeSpeedRequired / 8.7f;
        upgradeSpeedRequired = upgradeSpeedRequired + averageUpgradeSpeedRequired;
        txtUpgradeSpeedRequired.text = Math.Round(upgradeSpeedRequired, 2).ToString();
    }

    void OnClick_UpgradeIncome()
    {
        if (playerData.money < upgradeIncomeRequired)
        {
            return;
        }
        playerData.money -= upgradeIncomeRequired;
        CurrencyController.instance.DisplayCurrency(playerData.money);

        //Trung bình giá trị mới tăng theo level đã nâng cấp
        playerData.income += 1;
        txtCurrIncome.text = playerData.income.ToString();

        float averageUpgradeIncomeRequired = upgradeIncomeRequired / 10;
        upgradeIncomeRequired += averageUpgradeIncomeRequired;
        txtUpgradeIncomeRequired.text = Math.Round(upgradeIncomeRequired, 2).ToString();
    }

    private void OnDestroy()
    {
        Save();
    }
}
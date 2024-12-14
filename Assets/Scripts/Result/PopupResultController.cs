using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class PopupResultController : Singleton<PopupResultController>
{
    [SerializeField] private AssetReference topFrameRedReference;
    [SerializeField] private AssetReference topFrameBlueReference;

    [SerializeField] private Image imgTopFreame;
    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private Slider sliderProgress;
    [SerializeField] private TextMeshProUGUI txtCatsRescued;
    [SerializeField] private TextMeshProUGUI txtCompletedTime;
    [SerializeField] private TextMeshProUGUI txtMaxSpeed;
    [SerializeField] private Button btnConfirm;
    [SerializeField] private TextMeshProUGUI txtReward;
    [SerializeField] private Button btnClose;

    protected void Awake()
    {
        base.Awake();
        imgTopFreame = this.transform.GetChild(2).GetComponent<Image>();
        txtTitle = this.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        sliderProgress = this.transform.GetChild(4).GetComponent<Slider>();
        txtCatsRescued = this.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        txtCompletedTime = this.transform.GetChild(6).GetComponent<TextMeshProUGUI>();
        txtMaxSpeed = this.transform.GetChild(7).GetComponent<TextMeshProUGUI>();
        btnConfirm = this.transform.GetChild(8).GetComponent<Button>();
        txtReward = btnConfirm.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        btnClose = this.transform.GetChild(9).GetComponent<Button>();
        ChildActive(false);
    }

    private void Start()
    {
        //AsyncOperationHandle<IResourceLocator> handle = Addressables.InitializeAsync();
        //handle.Result.Key
    }
    private void OnEnable()
    {
        btnConfirm.onClick.AddListener(OnClick_Confirm);
        btnClose.onClick.AddListener(OnClick_Close);
    }

    private void OnDisable()
    {
        btnConfirm.onClick.RemoveAllListeners();
        btnClose.onClick.RemoveAllListeners();
    }

    public void DisplayResult(bool result, int catsRescued, int maxCats, float completedTime, float maxSpeed, float money)
    {
        SetTopFrame(result);
        SetTitle(result);
        SetCatsRescued(catsRescued, maxCats);
        SetCompletedTime(completedTime);
        SetMaxSpeed(maxSpeed);
        SetReward(money);
    }

    public void SetTopFrame(bool isSuccess)
    {
        if(isSuccess)
        {
            //imgTopFreame = 
        }
        else
        {

        }
    }

    public void SetTitle(bool isSuccess)
    {
        if (isSuccess)
        {
            txtTitle.text = "SUCCESS!";
        }
        else
        {
            txtTitle.text = "FAILURE!";
        }
    }

    public void SetCatsRescued(int amount, int maxCats)
    {
        sliderProgress.value = (float)amount / maxCats;
        txtCatsRescued.text = "Cats Rescued: " + amount.ToString() + "/" + maxCats.ToString();
    }

    public void SetCompletedTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        txtCompletedTime.text = "Completed Time: " + string.Format("{0:00}:{1:00}", minutes, seconds) + "s";
    }

    public void SetMaxSpeed(float maxSpeed)
    {
        txtMaxSpeed.text = "Max Speed: " + maxSpeed.ToString() + "m/s";
    }

    public void SetReward(float money)
    {
        txtReward.text = "GET " + Math.Round(money, 2).ToString();
    }
    void OnClick_Confirm()
    {
        SceneController.instance.LoadScene(SceneName.MainScene.ToString());
    }

    void OnClick_Close()
    {
        SceneController.instance.LoadScene(SceneName.MainScene.ToString());
    }
}

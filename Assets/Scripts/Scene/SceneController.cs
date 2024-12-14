using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : Singleton<SceneController>
{
    [SerializeField] private Image imgBackground;
    [SerializeField] private Slider sliderProgress;

    protected void Awake()
    {
        base.Awake();
        imgBackground = this.transform.GetChild(0).GetComponent<Image>();
        sliderProgress = this.transform.GetChild(1).GetComponent<Slider>();
        ChildActive(false);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "SceneLogin":
                break;
        }
    }

    public void LoadingSceen(bool isActive)
    {
        ChildActive(isActive);
    }

    public void LoadScene(string sceneName, Action func = null)
    {
        StartCoroutine(LoadSceneAsync(sceneName, func));
    }

    IEnumerator LoadSceneAsync(string sceneName, Action func = null)
    {
        //Tải scene và cập nhật thanh tiến trình 
        LoadingSceen(true);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            sliderProgress.value = progressValue;
            yield return null;
        }
        if (func != null)
        {
            func();
        }
        LoadingSceen(false);
    }
}

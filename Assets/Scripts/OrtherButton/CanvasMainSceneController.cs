using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMainSceneController : MyBehaviour
{
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnRace;

    void Awake()
    {
        btnSettings = this.transform.GetChild(0).GetComponent<Button>();
        btnRace = this.transform.GetChild(1).GetComponent<Button>();
    }

    void Start()
    {

    }

    private void OnEnable()
    {
        btnSettings.onClick.AddListener(OnClick_Settings);
        btnRace.onClick.AddListener(OnClick_Race);
    }

    private void OnDisable()
    {
        btnSettings.onClick.RemoveAllListeners();
        btnRace.onClick.RemoveAllListeners();
    }

    void Update()
    {

    }

    private void OnClick_Settings()
    {
        SettingsController.instance.ChildActive(true);
    }

    private void OnClick_Race()
    {
        SceneController.instance.LoadScene(SceneName.LevelScene.ToString());
    }
}

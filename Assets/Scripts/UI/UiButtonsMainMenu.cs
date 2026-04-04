using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiButtonsMainMenu : MonoBehaviour
{
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnSettings;
    [SerializeField] private Button _btnCredits;
    [SerializeField] private Button _btnExit;
    [SerializeField] private string _sceneToLoad;

    private void Start()
    {
        _btnPlay.onClick.AddListener(OnStartPressed);
        _btnSettings.onClick.AddListener(OnSettingsPressed);
        _btnCredits.onClick.AddListener(OnCreditsPressed);
        _btnExit.onClick.AddListener(OnExitPressed);
    }

    private void OnDestroy()
    {
        _btnPlay.onClick.RemoveAllListeners();
        _btnSettings.onClick.RemoveAllListeners();
        _btnCredits.onClick.RemoveAllListeners();
        _btnExit.onClick.RemoveAllListeners();
    }

    private void OnStartPressed()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }

    private void OnSettingsPressed()
    {

    }

    private void OnCreditsPressed()
    {

    }

    private void OnExitPressed()
    {
        Application.Quit();
    }
}


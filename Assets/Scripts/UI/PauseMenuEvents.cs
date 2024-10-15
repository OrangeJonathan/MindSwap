using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class PauseMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _pauseMenu;
    private UnityEngine.UIElements.Button _resumeButton, _settingsButton, _quitButton;

    private bool isPaused = false;

    void Start()
    {
        if (!MenuManager.IsInitialised)
        {
            MenuManager.Init();
            Debug.Log("initialize pause menu");
        }

        _document = GetComponent<UIDocument>();
        _document.enabled = true;
        _pauseMenu = _document.rootVisualElement.Q("PauseMenu");

        _resumeButton = _document.rootVisualElement.Q<UnityEngine.UIElements.Button>("ResumeGameButton");
        _settingsButton = _document.rootVisualElement.Q<UnityEngine.UIElements.Button>("OptionsGameButton");
        _quitButton = _document.rootVisualElement.Q<UnityEngine.UIElements.Button>("QuitGameButton");

        _resumeButton.RegisterCallback<ClickEvent>(OnResumeGameClick);
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsGameClick);
        _quitButton.RegisterCallback<ClickEvent>(OnQuitGameClick);

        _pauseMenu.style.display = DisplayStyle.None;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
        }
        else if (isPaused && Input.GetKeyDown(KeyCode.Escape)) // Ensure toggle with same key
        {
            UnPauseGame();
        }
    }

    private void OnResumeGameClick(ClickEvent evt)
    {
        UnPauseGame();
    }

    private void OnSettingsGameClick(ClickEvent evt)
    {
        MenuManager.OpenMenu(Menu.SETTINGS, _pauseMenu);
    }

    private void OnQuitGameClick(ClickEvent evt)
    {
        UnityEngine.Application.Quit();
    }

    private void OnDisable()
    {
        _resumeButton.UnregisterCallback<ClickEvent>(OnResumeGameClick);
        _settingsButton.UnregisterCallback<ClickEvent>(OnSettingsGameClick);
        _quitButton.UnregisterCallback<ClickEvent>(OnSettingsGameClick);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        MenuManager.OpenMenu(Menu.PAUSE, null);
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        isPaused = true;
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1f;
        MenuManager.CloseAllMenus();
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        isPaused = false;
    }
}

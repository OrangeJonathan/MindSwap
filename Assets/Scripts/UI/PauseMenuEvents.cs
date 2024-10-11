using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class PauseMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private UnityEngine.UIElements.Button _button;
    private VisualElement _pauseMenu;
    private bool isPaused = false;

    void Start()
    {
        _document = GetComponent<UIDocument>();
        _document.enabled = true;
        _pauseMenu = _document.rootVisualElement.Q("PauseMenu");
        _button = _document.rootVisualElement.Q("ResumeGameButton") as UnityEngine.UIElements.Button;
        _button.RegisterCallback<ClickEvent>(OnResumeGameClick);
        _pauseMenu.style.display = DisplayStyle.None; // Hide the pause menu
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

    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnResumeGameClick);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        _pauseMenu.style.display = DisplayStyle.Flex; // Show the pause menu
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        isPaused = true;
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1f;
        _pauseMenu.style.display = DisplayStyle.None; // Hide the pause menu
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        isPaused = false;
    }
}

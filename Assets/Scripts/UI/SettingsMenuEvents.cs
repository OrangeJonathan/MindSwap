using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsMenuEvents : MonoBehaviour
{
    private UIDocument _settingsDocument;
    private VisualElement _settingsMenu;
    private UnityEngine.UIElements.Button _backButton;

    private UnityEngine.UIElements.Slider _sensitivitySlider;

    void Start()
    {
        if (!MenuManager.IsInitialised)
            MenuManager.Init();
        
        if (!PlayerPrefsManager.IsInitialised)
            PlayerPrefsManager.Init();

        _settingsDocument = GetComponent<UIDocument>();
        _settingsDocument.enabled = true;
        _settingsMenu = _settingsDocument.rootVisualElement.Q("SettingsMenu");

        _backButton = _settingsDocument.rootVisualElement.Q<UnityEngine.UIElements.Button>("BackGameButton");
        _sensitivitySlider = _settingsDocument.rootVisualElement.Q<UnityEngine.UIElements.Slider>("SensitivitySlider");
    	_sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");; 

        _backButton.RegisterCallback<ClickEvent>(OnBackGameClick);
        _sensitivitySlider.RegisterCallback<ChangeEvent<float>>(OnSensitivitySliderValueChanged);
        
        _settingsMenu.style.display = DisplayStyle.None;
    }

    private void OnSensitivitySliderValueChanged(ChangeEvent<float> evt)
    {
        // default sensitivity is 100
        float sensitivity = PlayerPrefs.HasKey("Sensitivity") ? evt.newValue : 100;

        PlayerPrefsManager.UpdateSensitivity(sensitivity);
        _sensitivitySlider.value = sensitivity;
    }


    private void OnBackGameClick(ClickEvent evt)
    {
        MenuManager.OpenMenu(Menu.PAUSE, _settingsMenu);
    }

    private void OnDisable()
    {
        _backButton.UnregisterCallback<ClickEvent>(OnBackGameClick);
    }
}

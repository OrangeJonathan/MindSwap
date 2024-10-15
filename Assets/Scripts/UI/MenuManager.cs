using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class MenuManager
{
    public static bool IsInitialised { get; private set; }
    public static UIDocument pauseMenuDocument, settingsMenuDocument;
    private static VisualElement pauseMenu, settingsMenu;


    public static void Init()
    {
        pauseMenuDocument = GameObject.Find("PauseMenu").GetComponent<UIDocument>();
        settingsMenuDocument = GameObject.Find("SettingsMenu").GetComponent<UIDocument>();

        pauseMenuDocument.enabled = true;
        settingsMenuDocument.enabled = true;

        pauseMenu = pauseMenuDocument.rootVisualElement.Q<VisualElement>("PauseMenu");
        settingsMenu = settingsMenuDocument.rootVisualElement.Q<VisualElement>("SettingsMenu");

        IsInitialised = true;
    }

    public static void OpenMenu(Menu menu, VisualElement callingMenu)
    {
        if (!IsInitialised)
            Init();

        switch (menu)
        {
            case Menu.PAUSE:
                pauseMenu.style.display = DisplayStyle.Flex;
                break;
            case Menu.SETTINGS:
                settingsMenu.style.display = DisplayStyle.Flex;
                break;
        }

        if (callingMenu != null)
        {
            callingMenu.style.display = DisplayStyle.None;
        }
    }

    public static void CloseAllMenus()
    {
        if (!IsInitialised)
            Init();

        pauseMenu.style.display = DisplayStyle.None;
        settingsMenu.style.display = DisplayStyle.None;
    }
}

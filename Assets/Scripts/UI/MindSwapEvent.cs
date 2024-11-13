using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MindSwapEvent : MonoBehaviour
{
    public MindSwap mindSwapController;    
    public GameObject mindSwapMenu;
    public GameObject middleLabel;
    public GameObject[] buttons;
    public PlayerProperties playerProperties;

    public static bool IsOpened { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefsManager.IsInitialised)
            PlayerPrefsManager.Init();

        mindSwapController.OnChangeAbility += UpdateButtons;
        mindSwapMenu.SetActive(false);
    }

    void UpdateButtons()
    {
        int buttonIndex = 0;

        foreach (GameObject player in mindSwapController.players)
        {
            PlayerProperties playerProperties = player.GetComponent<PlayerProperties>();

            if (playerProperties.GetAbility() == mindSwapController.activePlayer)
                continue;

            if (buttonIndex >= buttons.Length)
            {
                Debug.LogWarning("Not enough buttons for all players.");
                break;
            }

            GameObject textObject = buttons[buttonIndex].transform.GetChild(0).gameObject;
            Text uiText = textObject.GetComponent<Text>();

            GameObject currentButton = null;
            currentButton = buttons[buttonIndex];

            if (!playerProperties.IsUnlocked)
            {
                currentButton.GetComponent<Image>().color = Color.gray;
                var button = currentButton.GetComponent<UnityEngine.UI.Button>();
                var colors = button.colors;
                colors.highlightedColor = new Color(0.4f, 0.4f, 0.4f, 1.0f);
                button.colors = colors;
            }
            else
            {
                currentButton.GetComponent<Image>().color = new Color(0.1372549f, 0.7372549f, 0.972549f);
                var button = currentButton.GetComponent<UnityEngine.UI.Button>();
                var colors = button.colors;
                colors.highlightedColor = new Color(0.1372549f, 0.8235294f, 0.972549f, 1.0f);
                button.colors = colors;
            }


            if (uiText != null)
            {
                uiText.text = player.name;
                Debug.Log("Setting button " + buttonIndex + " to player: " + player.name);
            }
            else
            {
                Debug.LogError("Text component not found on button " + buttonIndex);
            }

            buttonIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!mindSwapController.hasRemoteSwapper || MenuManager.IsAnyMenuOpen())
            return;
        
        if (Input.GetKeyDown(KeyCode.Q) && !IsOpened)
        {
            mindSwapMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerPrefsManager.LockMouseMovement();
            IsOpened = true;
        } 
        else if (Input.GetKeyUp(KeyCode.Q) && IsOpened)
        {
            mindSwapMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerPrefsManager.UnlockMouseMovement();
            IsOpened = false;
        }
    }


}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MindSwap : MonoBehaviour
{
    public List<GameObject> players;
    public event Action OnChangeAbility;
    public PlayerAbility.Ability activePlayer = PlayerAbility.Ability.PickUp;
    private int activePlayerIndex;
    private CharacterController activeController;
    private GameObject activeCamera;
    [SerializeField]
    private ItemObtain itemObtain;
    public bool hasRemoteSwapper = false;
    public List<SwapPanel> swapPanels = new();

    void Start()
    {
        itemObtain.OnObtain += ObtainsRemoteSwapper;

        // Subscribe to the SwapPanel's event
        foreach (var swapPanel in swapPanels)
        {
            swapPanel.OnSwapActivateAbility += SwapMind;
        }

        if (players.Count > 0)
        {
            activeCamera = players[(int)activePlayer].transform.Find("Player Camera").gameObject;
            activeController = players[(int)activePlayer].GetComponent<CharacterController>();

            ActivateController();
            ActivateCamera();
        }
        else
        {
            Debug.LogError("Player list is empty");
        }
    }

    // Method to be called when the event is raised, accepts the index from the event
    public void SwapMind(PlayerAbility.Ability ability)
    {
        GameObject switchTo = players[(int)ability].gameObject;

        if (activePlayer == ability)
        {
            Debug.Log("Cant swap to player that is currently active");
            return;
        }

        if (!switchTo.GetComponent<PlayerProperties>().IsUnlocked)
        {
            Debug.Log("Cant swap to player that is not unlocked");
            return;
        }
        
        if (ability < 0)
        {
            Debug.LogWarning($"Index {(int)ability} is out of range for players.");
            return;
        }

        activePlayer = ability;
        activePlayerIndex = (int)ability;
        activeCamera = players[activePlayerIndex].transform.Find("Player Camera").gameObject;
        activeController = players[activePlayerIndex].GetComponent<CharacterController>();

        OnChangeAbility?.Invoke();

        ActivateCamera();
        ActivateController();
    }

    public void SwapMindFromUI()
    {
        GameObject textObject = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;
        string ability = textObject.GetComponent<Text>().text;

        Debug.Log("Input string ?" + ability);

        PlayerAbility.Ability swapToAbility = GetAbilityFromString(ability);
        GameObject switchTo = players[(int)swapToAbility];

        if (activePlayer == GetAbilityFromString(ability))
        {
            Debug.Log("Cant swap to player that is currently active");
            return;
        }

        if (!switchTo.GetComponent<PlayerProperties>().IsUnlocked)
        {
            Debug.Log("Cant swap to player that is not unlocked");
            return;
        }

        activePlayer = swapToAbility;
        activePlayerIndex = (int)swapToAbility;

        activeCamera = players[activePlayerIndex].transform.Find("Player Camera").gameObject;
        activeController = players[activePlayerIndex].GetComponent<CharacterController>();

        OnChangeAbility?.Invoke();

        ActivateCamera();
        ActivateController();
    }

    PlayerAbility.Ability GetAbilityFromString(string ability)
    {
        bool hasParsed = Enum.TryParse(ability, out PlayerAbility.Ability parsedAbility);
        if (hasParsed)
        {
            Debug.Log(parsedAbility);
        }
        else
        {
            Debug.Log("Parse failed!" + ability);
        }

        return parsedAbility;
    }

    void ActivateCamera()
    {
        foreach (GameObject player in players) 
        {
            GameObject camera = player.transform.Find("Player Camera").gameObject;
            camera.SetActive(false);
        }
        activeCamera.SetActive(true);
    }

    void ActivateController()
    {
        foreach (GameObject player in players) 
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            controller.enabled = false;
        }
        activeController.enabled = true;
    }

    void ObtainsRemoteSwapper()
    {
        hasRemoteSwapper = true;
        itemObtain.OnObtain -= ObtainsRemoteSwapper;
    }

    void LosesRemoteSwapper()
    {
        hasRemoteSwapper = false;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid potential memory leaks
        foreach (var swapPanel in swapPanels)
        {
            swapPanel.OnSwapActivateAbility -= SwapMind;
        }
    }
}

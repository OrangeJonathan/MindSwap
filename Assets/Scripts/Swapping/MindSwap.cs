using System;
using System.Collections.Generic;
using UnityEngine;

public class MindSwap : MonoBehaviour
{
    public List<GameObject> players;
    public PlayerAbility.Ability activePlayer = 0;
    private int activePlayerIndex;
    private CharacterController activeController;
    private GameObject activeCamera;
    [SerializeField]
    private ItemObtain itemObtain;
    public bool hasRemoteSwapper = false;
    public List<SwapPanel> swapPanels = new();
    private int swapCount;

    void Start()
    {
        itemObtain.OnObtain += ObtainsRemoteSwapper;

        // Subscribe to the SwapPanel's event
        foreach (var swapPanel in swapPanels)
        {
            swapPanel.OnSwapActivate += SwapMind; // Subscribe to the event
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && hasRemoteSwapper)
        {
            SwapMind(); 
        }
    }

    // Method to be called when the event is raised, accepts the index from the event
    void SwapMind(PlayerAbility.Ability ability)
    {
        if (activePlayer == ability)
        {
            Debug.Log("Cant swap to player that is currently active");
            return;
        }
        if (ability < 0 || (int)ability >= players.Count)
        {
            Debug.LogWarning($"Index {(int)ability} is out of range for players.");
            return;
        }

        swapCount++;
        Debug.Log(swapCount);
        activePlayer = ability;
        activePlayerIndex = (int)ability;
        activeCamera = players[activePlayerIndex].transform.Find("Player Camera").gameObject;
        activeController = players[activePlayerIndex].GetComponent<CharacterController>();

        ActivateCamera();
        ActivateController();
    }

    void SwapMind()
    {
        if (!activeController.isGrounded) return;
        activePlayerIndex = (activePlayerIndex + 1) % players.Count;
        activePlayer = (PlayerAbility.Ability)activePlayerIndex;
        activeCamera = players[activePlayerIndex].transform.Find("Player Camera").gameObject;
        activeController = players[activePlayerIndex].GetComponent<CharacterController>();

        ActivateCamera();
        ActivateController();
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

    public int GetSwapCount()
    {
        return swapCount;
    }

    public void SetSwapCount(int count)
    {
        swapCount = count;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid potential memory leaks
        foreach (var swapPanel in swapPanels)
        {
            swapPanel.OnSwapActivate -= SwapMind;
        }
    }
}

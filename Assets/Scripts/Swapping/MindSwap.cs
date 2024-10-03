using System.Collections.Generic;
using UnityEngine;

public class MindSwap : MonoBehaviour
{
    public List<GameObject> players;
    public int activePlayer = 0;
    private CharacterController activeController;
    private GameObject activeCamera;
    public bool hasRemoteSwapper = false;
    public List<SwapPanel> swapPanels = new();

    void Start()
    {        
        // Subscribe to the SwapPanel's event
        foreach (var swapPanel in swapPanels)
        {
            swapPanel.OnSwapActivate += SwapMind; // Subscribe to the event
        }

        if (players.Count > 0)
        {
            activeCamera = players[activePlayer].transform.Find("Player Camera").gameObject;
            activeController = players[activePlayer].GetComponent<CharacterController>();

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
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            SwapMind(); // This can be used to swap mind without panels if needed
        }
    }

    // Method to be called when the event is raised, accepts the index from the event
    void SwapMind(int index)
    {
        if (activePlayer == index)
        {
            Debug.Log("Cant swap to player that is currently active");
            return;
        }
        if (index < 0 || index >= players.Count)
        {
            Debug.LogWarning($"Index {index} is out of range for players.");
            return;
        }

        activePlayer = index;
        activeCamera = players[activePlayer].transform.Find("Player Camera").gameObject;
        activeController = players[activePlayer].GetComponent<CharacterController>();

        ActivateCamera();
        ActivateController();
    }

    void SwapMind()
    {
        if (!activeController.isGrounded) return;
        activePlayer = (activePlayer + 1) % players.Count;
        activeCamera = players[activePlayer].transform.Find("Player Camera").gameObject;
        activeController = players[activePlayer].GetComponent<CharacterController>();

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

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid potential memory leaks
        foreach (var swapPanel in swapPanels)
        {
            swapPanel.OnSwapActivate -= SwapMind;
        }
    }
}

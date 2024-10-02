using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindSwap : MonoBehaviour
{
    public List<GameObject> players;
    public int activePlayer = 0;

    CharacterController activeController;
    GameObject activeCamera;

    void Start()
    {
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
            SwapMind();
        }
    }

    void SwapMind()
    {
        if (activeController.isGrounded)
        {
            activePlayer = (activePlayer + 1) % players.Count;
            activeCamera = players[activePlayer].transform.Find("Player Camera").gameObject;
            activeController = players[activePlayer].GetComponent<CharacterController>();

            ActivateCamera();
            ActivateController();
        }
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
}

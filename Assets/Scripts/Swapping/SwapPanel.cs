using System;
using System.Collections.Generic;
using UnityEngine;

public class SwapPanel : MonoBehaviour
{
    // Modify the event to pass an integer parameter (the swap index)
    public MindSwap mindSwapController;
    public event Action<PlayerAbility.Ability> OnSwapActivateAbility;

    public PlayerAbility.Ability swapToAbility = 0;
    public bool UnlockPlayer;
    private HashSet<Transform> playersInRange = new();

    void Update()
    {
        
        foreach (Transform player in playersInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed E");
                SwapPanelInteraction();
                break;
            }
        }
    }

    void SwapPanelInteraction()
    {
        Debug.Log("Interacted with panel for ability: " + swapToAbility);
        playersInRange.Clear();
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

        var currentAbility = mindSwapController.activePlayer;

        if (UnlockPlayer)
        {
            GameObject switchTo = mindSwapController.players[(int)swapToAbility].gameObject;
            switchTo.GetComponent<PlayerProperties>().UnlockPlayer();
        }

        OnSwapActivateAbility?.Invoke(swapToAbility);
        // Update after swap to switch between
        swapToAbility = currentAbility;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform playerTransform = other.transform;
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            playersInRange.Add(playerTransform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform playerTransform = other.transform;
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            playersInRange.Remove(playerTransform);
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class SwapPanel : MonoBehaviour
{
    // Modify the event to pass an integer parameter (the swap index)
    public event Action<int> OnSwapActivate;
    
    public int swapToIndex;
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
        Debug.Log("Interacted:" + swapToIndex);
        playersInRange.Clear();
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

        OnSwapActivate?.Invoke(swapToIndex);
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

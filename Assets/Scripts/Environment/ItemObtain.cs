using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObtain : MonoBehaviour
{
    public event Action OnObtain;

    public float interactionDistance = 2f;
    private bool itemObtained = false;
    private HashSet<Transform> playersInRange = new();

    void Update()
    {
        foreach (Transform player in playersInRange)
        {
            if (!itemObtained && Input.GetKeyDown(KeyCode.E))
            {
                ObtainItem();
                break;
            }
        }
    }

    void ObtainItem()
    {
        itemObtained = true;
        this.transform.GetChild(0).gameObject.SetActive(false);
        OnObtain?.Invoke(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !itemObtained)
        {
            Transform playerTransform = other.transform;
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true); // Set text to active
            playersInRange.Add(playerTransform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform playerTransform = other.transform;
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false); // Set text to inactive
            playersInRange.Remove(playerTransform);
        }
    }
}

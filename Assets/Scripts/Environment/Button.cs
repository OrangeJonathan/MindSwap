using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IActivatable
{
    public event Action OnActivate;   // Matches the interface's OnActivate
    public event Action OnDeactivate; // Not used, but needed for the interface

    public float interactionDistance = 2f;
    private bool buttonPressed = false;
    private HashSet<Transform> playersInRange = new();

    void Update()
    {
        foreach (Transform player in playersInRange)
        {
            if (!buttonPressed && Input.GetKeyDown(KeyCode.E))
            {
                ButtonPress();
                break;
            }
        }
    }

    void ButtonPress()
    {
        buttonPressed = true;
        this.GetComponentInChildren<Renderer>().material.color = Color.green;
        OnActivate?.Invoke(); // Fire the OnActivate event when button is pressed
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !buttonPressed)
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

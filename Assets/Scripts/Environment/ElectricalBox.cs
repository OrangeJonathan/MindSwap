using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalBox : MonoBehaviour, IActivatable
{
    public event Action OnActivate;
    public event Action OnDeactivate;

    private bool isElectricalBoxActivated = false;
    private HashSet<Transform> playersInRange = new();

    void Update()
    {
        foreach (Transform player in playersInRange)
        {
            if (!isElectricalBoxActivated && Input.GetKeyDown(KeyCode.E))
            {
                InteractWithElectricalBox();
                break;
            }
        }
    }

    void InteractWithElectricalBox()
    {
        isElectricalBoxActivated = true;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = Color.green;
        }
        OnActivate?.Invoke(); // Fire the OnActivate event when button is pressed
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerProperties>().GetAbility() == PlayerAbility.Ability.Electrician && !isElectricalBoxActivated)
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

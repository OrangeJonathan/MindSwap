using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public List<MonoBehaviour> activatorObjects;  // List of activator objects (can be PressButton, PressurePlate, etc.)
    private List<IActivatable> activators = new List<IActivatable>(); // List of activators that implement IActivatable
    private int activeCount = 0; // Track how many activators are active

    void Start()
    {
        // Loop through each activator object and assign the events
        foreach (var activatorObject in activatorObjects)
        {
            IActivatable activator = activatorObject as IActivatable;
            if (activator != null)
            {
                activators.Add(activator);
                activator.OnActivate += OnActivatorActivated;
                activator.OnDeactivate += OnActivatorDeactivated;
            }
        }
    }

    // Called when any activator is activated
    private void OnActivatorActivated()
    {
        activeCount++; // Increment the active count
        if (activeCount == 1) // Open the door when the first activator is triggered
        {
            OpenDoor();
        }
    }

    // Called when any activator is deactivated
    private void OnActivatorDeactivated()
    {
        activeCount--; // Decrement the active count
        if (activeCount == 0) // Close the door when no activators are active
        {
            CloseDoor();
        }
    }

    // Function to open the door
    private void OpenDoor()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Door is opening...");
    }

    // Function to close the door
    private void CloseDoor()
    {
        this.gameObject.SetActive(true);
        Debug.Log("Door is closing...");
    }

    private void OnDestroy()
    {
        // Unsubscribe from all events to avoid memory leaks
        foreach (var activator in activators)
        {
            activator.OnActivate -= OnActivatorActivated;
            activator.OnDeactivate -= OnActivatorDeactivated;
        }
    }
}

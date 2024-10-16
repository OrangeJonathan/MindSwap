using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public List<MonoBehaviour> activatorObjects;  // List of activator objects (can be PressButton, PressurePlate, etc.)
    private List<IActivatable> activators = new List<IActivatable>(); // List of activators that implement IActivatable
    private int activeCount = 0; // Track how many activators are active

    public int requiredActiveCount = 3;  // The number of activators that need to be active (3 players)

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
        Debug.Log("Activator Activated. Active Count: " + activeCount);
        
        // Check if the number of active players (activators) matches the required count (3 in this case)
        if (activeCount == requiredActiveCount)
        {
            EndLevel(); 
        }
    }

    private void OnActivatorDeactivated()
    {
        activeCount--; // Decrement the active count
        Debug.Log("Activator Deactivated. Active Count: " + activeCount);
    }

    private void EndLevel()
    {
        Debug.Log("All players are on the pressure plates. Level Finished!");
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

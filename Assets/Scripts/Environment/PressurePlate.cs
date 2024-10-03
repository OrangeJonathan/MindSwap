using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PressurePlate : MonoBehaviour, IActivatable
{
    public event Action OnActivate;
    public event Action OnDeactivate;

    void OnTriggerEnter(Collider collider)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        OnActivate?.Invoke(); // Fire the OnActivate event when the pressure plate is triggered
    }

    void OnTriggerExit(Collider collider)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        OnDeactivate?.Invoke(); // Optionally, fire the OnDeactivate event when the pressure plate is released
    }
}

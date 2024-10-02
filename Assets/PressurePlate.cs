using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public event Action OnPressurePlateActivated;
    public event Action OnPressurePlateDeactivated;


    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider);
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        OnPressurePlateActivated?.Invoke();
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log(collider);
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        OnPressurePlateDeactivated?.Invoke();
    }
}

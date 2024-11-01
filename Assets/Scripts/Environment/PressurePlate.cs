using System;
using Unity.VisualScripting;
using UnityEngine;
using ColorUtility = UnityEngine.ColorUtility;

public class PressurePlate : MonoBehaviour, IActivatable
{
    public event Action OnActivate;
    public event Action OnDeactivate;

    private int objectsOnPlate = 0; // Track the number of objects on the plate

    void OnTriggerEnter(Collider collider)
    {
        if (objectsOnPlate == 0)
        {
            ActivatePlate();
        }
        objectsOnPlate++;
    }

    void OnTriggerExit(Collider collider)
    {
        objectsOnPlate--;
        if (objectsOnPlate == 0)
        {
            DeactivatePlate();
        }
    }

    private void ActivatePlate()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = Color.green;
        }
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        OnActivate?.Invoke();
    }

    private void DeactivatePlate()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = HexToColor("#1D1D1D");
        }
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        OnDeactivate?.Invoke();
    }

    private Color HexToColor(string hex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }
        else
        {
            Debug.LogWarning("Invalid hex color code provided.");
            return Color.white; // Default color if hex is invalid
        }
    }
}

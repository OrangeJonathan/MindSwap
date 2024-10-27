using System;
using Unity.VisualScripting;
using UnityEngine;
using ColorUtility = UnityEngine.ColorUtility;

public class PressurePlate : MonoBehaviour, IActivatable
{
    public event Action OnActivate;
    public event Action OnDeactivate;

    private Collider currentCollider;

    void OnTriggerEnter(Collider collider)
    {
        if (collider != currentCollider && currentCollider != null) return;
        currentCollider = collider;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = Color.green;
        }
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        OnActivate?.Invoke(); // Fire the OnActivate event when the pressure plate is triggered
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider != currentCollider) return;
        currentCollider = null;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = HexToColor("#1D1D1D");
        }
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        OnDeactivate?.Invoke(); // Optionally, fire the OnDeactivate event when the pressure plate is released
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

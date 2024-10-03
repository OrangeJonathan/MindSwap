using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatform : MonoBehaviour
{
    public MonoBehaviour activatorObject; // This can be either PressButton or PressurePlate
    private IActivatable activator;

    void Start()
    {
        activator = activatorObject as IActivatable;
        if (activator != null)
        {
            activator.OnActivate += EnablePlatform;
        }
    }

    private void EnablePlatform()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        // Optionally, you can disable further activation or remove the event if needed
        activator.OnActivate -= EnablePlatform;
    }

    private void OnDestroy()
    {
        if (activator != null)
        {
            activator.OnActivate -= EnablePlatform;
        }
    }
}

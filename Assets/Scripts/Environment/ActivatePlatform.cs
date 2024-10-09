using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatform : MonoBehaviour
{
    public MonoBehaviour activatorObject;
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
        activator.OnActivate -= EnablePlatform;
        activator.OnDeactivate += DisablePlatform;
    }

    private void DisablePlatform()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        activator.OnActivate += EnablePlatform;
        activator.OnDeactivate -= DisablePlatform;
    }

    private void OnDestroy()
    {
        if (activator != null)
        {
            activator.OnActivate -= EnablePlatform;
            activator.OnDeactivate -= DisablePlatform;
        }
    }
}

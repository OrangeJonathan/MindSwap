using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public MonoBehaviour activatorObject;
    private IActivatable activator;

    void Start()
    {
        activator = activatorObject as IActivatable;
        if (activator != null)
        {
            activator.OnActivate += OpenDoor;
        }
    }

    private void OpenDoor()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Door is opening...");
        activator.OnActivate -= OpenDoor;
        activator.OnDeactivate += CloseDoor;

    }

    private void CloseDoor()
    {
        this.gameObject.SetActive(true);
        activator.OnActivate += OpenDoor;
        activator.OnDeactivate -= CloseDoor;
    }

    private void OnDestroy()
    {
        // Unsubscribe when the object is destroyed to avoid memory leaks
        if (activator != null)
        {
            activator.OnActivate -= OpenDoor;
            activator.OnDeactivate -= CloseDoor;
        }
    }
}

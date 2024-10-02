using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public PressurePlate pressurePlate; // Assign this in the inspector to specify which plate opens this door

    void Start()
    {
        // Subscribe to the pressure plate's event
        if (pressurePlate != null)
        {
            pressurePlate.OnPressurePlateActivated += OpenDoor;
        }
    }

    private void OpenDoor()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Door is opening...");
        pressurePlate.OnPressurePlateActivated -= OpenDoor;
        pressurePlate.OnPressurePlateDeactivated += CloseDoor;

    }

    private void CloseDoor()
    {
        this.gameObject.SetActive(true);
        pressurePlate.OnPressurePlateActivated += OpenDoor;
        pressurePlate.OnPressurePlateDeactivated -= CloseDoor;
    }

    private void OnDestroy()
    {
        // Unsubscribe when the object is destroyed to avoid memory leaks
        if (pressurePlate != null)
        {
            pressurePlate.OnPressurePlateActivated -= OpenDoor;
            pressurePlate.OnPressurePlateDeactivated -= CloseDoor;
        }
    }
}

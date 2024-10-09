using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public event Action OnPickUp; 
    public event Action OnRelease;

    public float interactionDistance = 2f;
    private bool itemPickedUp = false;
    private Collider currentCollider;
    private HashSet<Transform> playersInRange = new();

    private Transform cubeParent;  // Reference to the cube's main GameObject (parent)

    void Start()
    {
        // Get the parent of the trigger object, which is the main cube
        cubeParent = transform.parent;  
    }

    void Update()
    {
        foreach (Transform player in playersInRange)
        {
            if (!itemPickedUp && Input.GetKeyDown(KeyCode.Mouse0))
            {
                PickUpItem();
                break;
            }
            if (itemPickedUp && Input.GetKeyDown(KeyCode.Mouse0))
            {
                ReleaseItem();      
                break;
            }        
        }

    }

    void PickUpItem()
    {
        itemPickedUp = true;

        // Move the parent cube to the front of the player in their local space
        Vector3 offsetPosition = currentCollider.transform.position + currentCollider.transform.forward * 5; // 3 units in front of the player
        cubeParent.position = offsetPosition;

        Rigidbody cubeRigidbody = cubeParent.GetComponent<Rigidbody>();
        if (cubeRigidbody != null)
        {
            cubeRigidbody.useGravity = false;
        }

        // Set the cube to be a child of the player who picked it up
        cubeParent.SetParent(currentCollider.transform);
        OnPickUp?.Invoke();
    }

    void ReleaseItem()
    {
        itemPickedUp = false;

        // Release the cube from being a child of the player
        cubeParent.SetParent(null);

        Rigidbody cubeRigidbody = cubeParent.GetComponent<Rigidbody>();
        if (cubeRigidbody != null)
        {
            cubeRigidbody.useGravity = true;
        }

        OnRelease?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject.GetComponent<PlayerProperties>().GetAbility() == PlayerAbility.Ability.PickUp)
        {
            currentCollider = other;
            Transform playerTransform = other.transform;
            playersInRange.Add(playerTransform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject.GetComponent<PlayerProperties>().GetAbility() == PlayerAbility.Ability.PickUp)
        {
            currentCollider = null;
            Transform playerTransform = other.transform;
            playersInRange.Remove(playerTransform);
        }
    }
}

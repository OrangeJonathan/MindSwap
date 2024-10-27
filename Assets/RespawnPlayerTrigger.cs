using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnPlayerTrigger : MonoBehaviour
{

    [SerializeField]
    private Transform respawnPoint;


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("respawning");
            collider.GetComponent<Rigidbody>().isKinematic = true;
            collider.transform.position = respawnPoint.position;
            collider.GetComponent<Rigidbody>().isKinematic = false;
            collider.GetComponent<Rigidbody>().isKinematic = true;

            Debug.Log("respawned");
        }
    }
}

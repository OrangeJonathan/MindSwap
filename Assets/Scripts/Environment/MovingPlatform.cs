using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public MonoBehaviour activatorObject;
    private IActivatable activator;
    public Transform startingPosition; 
    public Transform endingPosition;   
    public float speed = 2f;           
    private bool isMoving = false;     
    private bool movingToEnd = true;   

    void Start()
    {
        activator = activatorObject as IActivatable;
        if (activator != null)
        {
            activator.OnActivate += MovePlatformToEnd;
            activator.OnDeactivate += MovePlatformToStart;
        }
    }

    void Update()
    {
        if (isMoving)
        {
            if (movingToEnd)
            {
                transform.position = Vector3.MoveTowards(transform.position, endingPosition.position, speed * Time.deltaTime);
                
                if (Vector3.Distance(transform.position, endingPosition.position) < 0.1f)
                {
                    isMoving = false; 
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startingPosition.position, speed * Time.deltaTime);
                
                if (Vector3.Distance(transform.position, startingPosition.position) < 0.1f)
                {
                    isMoving = false; 
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player: " + other.name);
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is childed");
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

    private void MovePlatformToEnd()
    {
        movingToEnd = true;  
        isMoving = true;     

        activator.OnActivate -= MovePlatformToEnd;
        activator.OnDeactivate += MovePlatformToStart;
    }

    private void MovePlatformToStart()
    {
        movingToEnd = false; 
        isMoving = true;     

        activator.OnActivate += MovePlatformToEnd;
        activator.OnDeactivate -= MovePlatformToStart;
    }

    private void OnDestroy()
    {
        if (activator != null)
        {
            activator.OnActivate -= MovePlatformToEnd;
            activator.OnDeactivate -= MovePlatformToStart;
        }
    }
}

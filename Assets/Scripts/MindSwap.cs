using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindSwap : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public bool character1Active = true;

    CharacterController controller1;
    CharacterController controller2;
    GameObject cam1;
    GameObject cam2;


    void Start()
    {
        cam1 = player1.transform.Find("Player Camera").gameObject;
        cam2 = player2.transform.Find("Player Camera").gameObject;
        controller1 = player1.GetComponent<CharacterController>();
        controller2 = player2.GetComponent<CharacterController>();

        player2.GetComponent<CharacterController>().enabled = false;

        cam1.SetActive(true);
        cam2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            SwapMind();
        }
    }

    public void SwapMind()
    {
        if (character1Active && controller1.isGrounded)
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
            player1.GetComponent<CharacterController>().enabled = false;
            player2.GetComponent<CharacterController>().enabled = true;
            character1Active = false;
        }
        else if (!character1Active && controller2.isGrounded)
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
            player1.GetComponent<CharacterController>().enabled = true;
            player2.GetComponent<CharacterController>().enabled = false;
            character1Active = true;
        }
    }
}

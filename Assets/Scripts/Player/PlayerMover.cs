using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public CharacterController controller;
    public PlayerAbility.Ability playerAbility;
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 2.5f;
    private Vector3 velocity;

    [Header("Ground")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;



    void Start()
    {
        switch (playerAbility)
        {
            case PlayerAbility.Ability.HighJump:
                jumpHeight *= 3;
                break;
                
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (controller.enabled == false) 
        {
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        MovePlayer();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void MovePlayer()
    {
        float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zMove = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        Vector3 move = transform.right * xMove + transform.forward * zMove;

        controller.Move(move);
    }
}

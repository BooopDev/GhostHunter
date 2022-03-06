using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0.1f,3)]
    public float walkSpeed = 1;
    [Range(0.1f,1f)]
    public float sneakSpeed = 0.5f;
    public float accelerationSpeed = 1.5f;
    public float deccelerationSpeed = 0.8f;
    public float gravityModifier = -9.81f;
    public Transform groundCheck;
    public float groundCheckDistance = 0.3f;
    public LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private float moveSpeed;
    [HideInInspector]
    public bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if(!GameOver.hasGameEnded)
        {
            Movement();
            Gravity();
        }
    }

    private void Movement()
    {
        float moveX = Input.GetAxis("Horizontal"); float moveZ = Input.GetAxis("Vertical");

        Vector3 move = Vector3.ClampMagnitude((transform.right * moveX) + (transform.forward * moveZ), 1);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (moveSpeed > sneakSpeed)
            {
                moveSpeed -= deccelerationSpeed * Time.fixedDeltaTime;
                moveSpeed = Mathf.Clamp(moveSpeed, sneakSpeed, walkSpeed);
            }
        }
        else
        {
            if (moveSpeed < walkSpeed)
            {
                moveSpeed += accelerationSpeed * Time.fixedDeltaTime;
                moveSpeed = Mathf.Clamp(moveSpeed, sneakSpeed, walkSpeed);
            }
        }
        if(GhostHitCheck.hasGhostBeenHit)
        {
            GameObject ghost = GameObject.FindGameObjectWithTag("Enemy");
            Vector3 offset = ghost.transform.position - transform.position;
            if(offset.magnitude > 1)
            {
                offset = offset.normalized * (moveSpeed / 2);
                Vector3 lerpedMove = Vector3.Lerp(offset, move, 0.25f);
                controller.Move((lerpedMove * 5) * Time.fixedDeltaTime);
            }
        }
        else
        {
            controller.Move(move * (moveSpeed * 5) * Time.fixedDeltaTime);
        }
    }

    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravityModifier * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

}

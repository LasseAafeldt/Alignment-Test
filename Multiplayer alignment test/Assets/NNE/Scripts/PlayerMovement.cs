using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravityModifier = 2f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckRadius = .2f;

    private bool isGrounded = false;
    private CharacterController controller;
    private Vector3 velocity;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckRadius, groundMask);

        //reset fall if we are grounded
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;

        //apply move input
        controller.Move(movement * moveSpeed * Time.deltaTime);

        //apply gravity
        velocity.y += Physics.gravity.y * gravityModifier * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        //draw ground check gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, groundCheckRadius);
    }
}

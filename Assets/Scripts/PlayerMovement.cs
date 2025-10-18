using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float jumpForce = 10f;
    InputAction moveAction;
    InputAction jumpAction;
    Rigidbody2D myRigidbody2D;
    BoxCollider2D myFeetCollider2D;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }


    void Update()
    {
        Move();
        Jump();
    }


    void Move()
    {
        // if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Platform"))) return;

        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        myRigidbody2D.linearVelocity = new Vector2(moveValue.x * movementSpeed, myRigidbody2D.linearVelocity.y);
    }
    
    void Jump()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Platform"))) return;

        if (jumpAction.IsPressed())
        {
            myRigidbody2D.linearVelocity = new Vector2(0f, jumpForce);
        }
    }
}

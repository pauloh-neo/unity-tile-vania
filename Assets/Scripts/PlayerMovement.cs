using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float jumpForce = 10f;
    InputAction moveAction;
    Vector2 moveValue;
    InputAction jumpAction;
    Rigidbody2D myRigidbody2D;
    BoxCollider2D myFeetCollider2D;
    Animator myAnimator;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }


    void Update()
    {
        Move();
        Jump();
        Flip();
    }


    void Move()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        myRigidbody2D.linearVelocity = new Vector2(moveValue.x * movementSpeed, myRigidbody2D.linearVelocity.y);
        bool hasHorizontalSpeed = Mathf.Abs(moveValue.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }

    void Jump()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Platform"))) return;

        if (jumpAction.IsPressed())
        {
            myRigidbody2D.linearVelocity = new Vector2(0f, jumpForce);
        }
    }
    
    void Flip()
    {
        bool hasHorizontalSpeed = Mathf.Abs(moveValue.x) > Mathf.Epsilon;

        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(moveValue.x), 1f);
        }
    }
}

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
    CapsuleCollider2D myBodyCollider2D;
    Animator myAnimator;
    float playerGravity;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        playerGravity = myRigidbody2D.gravityScale;
    }


    void Update()
    {
        Move();
        Jump();
        Flip();
        Climb();
        
    }


    void Move()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        myRigidbody2D.linearVelocity = new Vector2(moveValue.x * movementSpeed, myRigidbody2D.linearVelocity.y);
        bool hasHorizontalSpeed = Mathf.Abs(moveValue.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }

    void Climb()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRigidbody2D.linearVelocity = new Vector2(myRigidbody2D.linearVelocity.x, moveValue.y * movementSpeed);
            bool hasVerticalSpeed = Mathf.Abs(moveValue.y) > Mathf.Epsilon;
            myRigidbody2D.gravityScale = 0;
            myAnimator.SetBool("isClimbing", hasVerticalSpeed);
        }
        else
        {
            myRigidbody2D.gravityScale = playerGravity;
            myAnimator.SetBool("isClimbing", false);
        }

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

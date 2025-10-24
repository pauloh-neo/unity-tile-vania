using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] bool isAlive;
    [SerializeField] Vector2 deathKick = new Vector2(10f,10f);
    [SerializeField] float fadeSpeed = 1f;
    [SerializeField] float targetAlpha = 0;
    [SerializeField] Transform gun;
    [SerializeField] GameObject bullet;
    InputAction moveAction;
    Vector2 moveValue;
    InputAction jumpAction;
    Rigidbody2D myRigidbody2D;
    BoxCollider2D myFeetCollider2D;
    CapsuleCollider2D myBodyCollider2D;
    SpriteRenderer mySpriterenderer;
    Animator myAnimator;
    float playerGravity;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        mySpriterenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        
        playerGravity = myRigidbody2D.gravityScale;
        isAlive = true;
        
    }


    void Update()
    {
        if (!isAlive)
        {
            FadingEffect();
            return;
        }
        
        Move();
        Jump();
        Flip();
        Climb();
        Die();
        
        
    }


    void Move()
    {
        if (!isAlive) return;
        moveValue = moveAction.ReadValue<Vector2>();
        myRigidbody2D.linearVelocity = new Vector2(moveValue.x * movementSpeed, myRigidbody2D.linearVelocity.y);
        bool hasHorizontalSpeed = Mathf.Abs(moveValue.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }

    void OnAttack(InputValue value)
    {
        if (!isAlive) return;
       
        Instantiate(bullet, gun.position, transform.rotation);
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

    void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")) || myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody2D.linearVelocity = deathKick;
        }
        
    }

    void FadingEffect()
    {
        Color color = mySpriterenderer.color;
        color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
        mySpriterenderer.color = color;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Coin":
                Destroy(collision.gameObject);
                break;
        }
    }
    
}

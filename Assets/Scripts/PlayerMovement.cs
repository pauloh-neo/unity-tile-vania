using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    InputAction moveAction;
    Rigidbody2D myRigidbody2D;
    BoxCollider2D myFeetCollider2D;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }


    void Update()
    {
        Move();
    }
    
     
    void Move()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Platform"))) return;
        
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        myRigidbody2D.linearVelocity = new Vector2(moveValue.x * movementSpeed, myRigidbody2D.linearVelocity.y);
    }
}

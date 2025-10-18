using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    Rigidbody2D myRigidbody2d;
    BoxCollider2D myBoxcollider2d;

    void Start()
    {
        myRigidbody2d = GetComponent<Rigidbody2D>();
        myBoxcollider2d = GetComponent<BoxCollider2D>();
       
    }


    void Update()
    {
        Move();
    }

    void Move()
    {
        myRigidbody2d.linearVelocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
            moveSpeed = -moveSpeed;
            FlipEnemyFacing(); 
     
    }
    
    void FlipEnemyFacing()
    {
        if (myBoxcollider2d.IsTouchingLayers(LayerMask.NameToLayer("Player")))
        {
            return;
        }
        transform.localScale = new Vector2(-Mathf.Sign(myRigidbody2d.linearVelocity.x), 1f);
    }
    
}

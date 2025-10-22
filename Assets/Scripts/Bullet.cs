using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float delayToDestroy = 2f;
    float xSpeed;
    Rigidbody2D myRigidbody2D;
    PlayerMovement playerMovement;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        xSpeed = playerMovement.transform.localScale.x * bulletSpeed;
    }


    void Update()
    {
        myRigidbody2D.linearVelocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }

        Destroy(gameObject, delayToDestroy);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, delayToDestroy);
    }
}

using System;
using Unity.VisualScripting;
using UnityEngine;

public class CoinPickUp: MonoBehaviour
{
    [SerializeField] AudioClip coinSfx;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(coinSfx, transform.position);
            FindFirstObjectByType<GameSession>().Score();
        }
    }

}

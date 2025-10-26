using System;
using Unity.VisualScripting;
using UnityEngine;

public class CoinPickUp: MonoBehaviour
{
    [SerializeField] AudioClip coinSfx;
    [SerializeField] int scorePoints = 100;

    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wasCollected = true;
            FindFirstObjectByType<GameSession>().Score(scorePoints);
            AudioSource.PlayClipAtPoint(coinSfx, transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

}

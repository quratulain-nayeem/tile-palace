using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX; 
    [SerializeField] int pointForCoinPickup = 100;

    bool wasCollected = false;
       void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;
            FindAnyObjectByType<GameSession>().AddToScore(pointForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickupSFX, transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}

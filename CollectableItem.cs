using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public void OnCollected()
    {
        // Destroy the collected object
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Additional logic for when the player enters the collectible's trigger can be added here if needed.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Additional logic for when the player exits the collectible's trigger can be added here if needed.
        }
    }
}

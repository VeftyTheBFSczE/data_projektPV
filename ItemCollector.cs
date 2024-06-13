using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    public int itemCount = 0;
    public int targetItemCount = 4;
    public TextMeshProUGUI collectionText;  // Assign in the inspector
    public TextMeshProUGUI promptText;  // Assign in the inspector
    public GameObject trigger;  // Assign the trigger object in the inspector

    private GameObject currentCollectible = null;

    private void Start()
    {
        // Initialize text
        collectionText.text = "";
        promptText.text = "";
        trigger.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentCollectible != null)
        {
            CollectItem();
        }
    }

    private void CollectItem()
    {
        if (currentCollectible != null)
        {
            // Increment item count and update the text
            itemCount++;
            collectionText.text = "Item collected";

            // Notify the collectible that it has been collected
            currentCollectible.GetComponent<CollectibleItem>().OnCollected();

            // Clear the reference and prompt text
            currentCollectible = null;
            promptText.text = "";

            // Check if the required number of items have been collected
            if (itemCount >= targetItemCount)
            {
                ActivateTrigger();
            }

            // Clear the collection message after a short delay
            Invoke("ClearCollectionText", 2.0f);
        }
    }

    private void ClearCollectionText()
    {
        collectionText.text = "";
    }

    private void ActivateTrigger()
    {
        // Activate the trigger
        trigger.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            currentCollectible = other.gameObject;
            promptText.text = "Press E to collect";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            currentCollectible = null;
            promptText.text = "";
        }
    }
}

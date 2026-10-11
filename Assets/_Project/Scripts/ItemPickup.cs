using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private string itemName = "Item";
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private float destroyDelay = 0.5f;

    private PlayerInventory inventory;
    private bool collected = false;

    private void Start()
    {
        inventory = FindFirstObjectByType<PlayerInventory>();
        if (inventory == null)
            Debug.LogError("PlayerInventory no encontrado");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        GameManager manager = FindFirstObjectByType<GameManager>();
        if (manager == null || !manager.IsGameActive()) return;
        if (other.CompareTag("Player"))
        {
            collected = true;
            CollectItem();
        }
    }

    private void CollectItem()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory es null en CollectItem");
            return;
        }

        bool added = inventory.AddItem(itemName);
        Debug.Log($"Item recolectado: {itemName}, Total: {inventory.GetItemCount()}/3");

        if (added)
        {
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        float elapsed = 0f;
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null) yield break;

        Color originalColor = renderer.material.color;

        while (elapsed < destroyDelay)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / destroyDelay);
            Color newColor = originalColor;
            newColor.a = alpha;
            renderer.material.color = newColor;

            yield return null;
        }

        Destroy(gameObject);
    }
}

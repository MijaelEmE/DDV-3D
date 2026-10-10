using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private string itemName = "Item";
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private float destroyDelay = 0.5f;

    private PlayerInventory inventory;
    private bool collected = false;

    private void Start()
    {
        inventory = FindObjectOfType<PlayerInventory>();
        if (inventory == null)
            Debug.LogError("PlayerInventory no encontrado en la escena");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;
        CollectItem();
    }

    private void CollectItem()
    {
        if (inventory != null && inventory.AddItem(itemName))
        {
            // Reproducir sonido
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // Efecto de desaparición
            StartCoroutine(FadeOut());
        }
    }

    private System.Collections.IEnumerator FadeOut()
    {
        float elapsed = 0f;
        Renderer renderer = GetComponent<Renderer>();
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

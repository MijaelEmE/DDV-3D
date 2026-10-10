using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Text itemCountText;
    [SerializeField] private Text healthText;
    [SerializeField] private Text objectiveText;
    [SerializeField] private Image healthBar;

    private PlayerInventory inventory;
    private PlayerHealth playerHealth;
    private ObjectiveManager objectiveManager;

    private void Start()
    {
        inventory = FindObjectOfType<PlayerInventory>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();

        if (inventory != null)
        {
            inventory.onItemCollected += UpdateItemCount;
            Debug.Log("GameUI: Conectado a PlayerInventory");
        }
        else
            Debug.LogError("GameUI: PlayerInventory no encontrado");

        if (playerHealth != null)
        {
            playerHealth.onHealthChanged += UpdateHealth;
            Debug.Log("GameUI: Conectado a PlayerHealth");
        }
        else
            Debug.LogError("GameUI: PlayerHealth no encontrado");

        UpdateItemCount(0);
        UpdateHealth(playerHealth != null ? playerHealth.GetHealth() : 3);
        UpdateObjectiveText();
    }

    private void UpdateItemCount(int count)
    {
        if (itemCountText == null)
        {
            Debug.LogError("itemCountText no asignado en GameUI");
            return;
        }

        int maxItems = inventory != null ? inventory.GetMaxItems() : 3;
        itemCountText.text = $"Items: {count}/{maxItems}";
        itemCountText.color = (count == maxItems) ? Color.green : Color.white;

        Debug.Log($"UI Actualizado: Items {count}/{maxItems}");
    }

    private void UpdateHealth(int health)
    {
        if (healthText != null)
        {
            healthText.text = $"Vidas: {health}";
            Debug.Log($"UI Actualizado: Vidas {health}");
        }
        else
            Debug.LogError("healthText no asignado");

        if (healthBar != null && playerHealth != null)
        {
            float healthPercent = (float)health / playerHealth.GetMaxHealth();
            healthBar.fillAmount = healthPercent;

            if (healthPercent > 0.5f)
                healthBar.color = Color.green;
            else if (healthPercent > 0.25f)
                healthBar.color = Color.yellow;
            else
                healthBar.color = Color.red;
        }
    }

    private void UpdateObjectiveText()
    {
        if (objectiveText != null && objectiveManager != null)
        {
            objectiveText.text = objectiveManager.GetObjectiveText();
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image portalIndicator;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color completedColor = Color.green;

    private PlayerInventory inventory;
    private PlayerHealth playerHealth;
    private ObjectiveManager objectiveManager;

    private void Start()
    {
        inventory = FindObjectOfType<PlayerInventory>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();

        if (inventory != null)
            inventory.onItemCollected += UpdateItemCount;

        if (playerHealth != null)
            playerHealth.onHealthChanged += UpdateHealth;

        if (objectiveManager != null)
        {
            UpdateObjectiveText();
        }

        UpdateItemCount(0);
        UpdateHealth(playerHealth != null ? playerHealth.GetHealth() : 0);
    }

    private void UpdateItemCount(int count)
    {
        if (itemCountText != null)
        {
            int maxItems = inventory != null ? inventory.GetMaxItems() : 3;
            itemCountText.text = $"Items: {count}/{maxItems}";
            itemCountText.color = (count == maxItems) ? completedColor : normalColor;
        }
    }

    private void UpdateHealth(int health)
    {
        if (healthText != null)
            healthText.text = $"Vidas: {health}";

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
            objectiveText.text = objectiveManager.GetObjectiveText();
    }

    private void Update()
    {
        if (portalIndicator != null && objectiveManager != null)
        {
            portalIndicator.enabled = objectiveManager.IsPortalActive();
            if (portalIndicator.enabled)
                portalIndicator.text = "Portal Abierto!";
        }
    }
}

using UnityEngine;

public class PortalExit : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private GameManager gameManager;
    private ObjectiveManager objectiveManager;
    private PlayerInventory inventory;
    private bool triggered = false;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        objectiveManager = FindFirstObjectByType<ObjectiveManager>();
        inventory = FindFirstObjectByType<PlayerInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        TryExit(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TryExit(other);
    }

    private void TryExit(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag(playerTag)) return;
        // The portal starts inactive, so resolve references when it is used too.
        if (gameManager == null) gameManager = FindFirstObjectByType<GameManager>();
        if (objectiveManager == null) objectiveManager = FindFirstObjectByType<ObjectiveManager>();
        if (inventory == null) inventory = FindFirstObjectByType<PlayerInventory>();
        if (gameManager == null || !gameManager.IsGameActive()) return;
        if (objectiveManager == null || !objectiveManager.IsPortalActive()) return;
        if (inventory == null || !inventory.HasAllItems()) return;

        triggered = true;

        if (gameManager != null)
            gameManager.Victory();
    }
}

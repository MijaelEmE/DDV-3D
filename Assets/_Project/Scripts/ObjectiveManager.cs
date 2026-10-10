using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private GameObject portalObject;
    [SerializeField] private Light portalLight;
    [SerializeField] private AudioClip portalActivationSound;
    [SerializeField] private string objectiveText = "Encuentra 3 objetos antiguos";

    private PlayerInventory inventory;
    private bool portalActive = false;

    public System.Action onObjectiveComplete;

    private void Start()
    {
        inventory = FindObjectOfType<PlayerInventory>();

        if (inventory != null)
            inventory.onItemCollected += CheckObjective;

        if (portalObject != null)
            portalObject.SetActive(false);
    }

    private void CheckObjective(int itemCount)
    {
        if (inventory.HasAllItems() && !portalActive)
            ActivatePortal();
    }

    private void ActivatePortal()
    {
        portalActive = true;

        if (portalObject != null)
            portalObject.SetActive(true);

        if (portalLight != null)
            portalLight.enabled = true;

        if (portalActivationSound != null)
            AudioSource.PlayClipAtPoint(portalActivationSound, transform.position);

        onObjectiveComplete?.Invoke();
        Debug.Log("¡Portal Abierto! Dirígete hacia él para escapar.");
    }

    public bool IsPortalActive() => portalActive;
    public string GetObjectiveText() => objectiveText;
}

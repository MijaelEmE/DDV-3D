using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxItems = 3;
    private List<string> collectedItems = new List<string>();

    public System.Action<int> onItemCollected;
    public System.Action onInventoryFull;

    private static PlayerInventory instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public bool AddItem(string itemName)
    {
        if (collectedItems.Count >= maxItems)
        {
            onInventoryFull?.Invoke();
            return false;
        }

        collectedItems.Add(itemName);
        onItemCollected?.Invoke(collectedItems.Count);
        return true;
    }

    public int GetItemCount() => collectedItems.Count;
    public int GetMaxItems() => maxItems;
    public bool IsInventoryFull() => collectedItems.Count >= maxItems;
    public bool HasAllItems() => collectedItems.Count == maxItems;

    public void ClearInventory() => collectedItems.Clear();
    public List<string> GetItems() => new List<string>(collectedItems);
}

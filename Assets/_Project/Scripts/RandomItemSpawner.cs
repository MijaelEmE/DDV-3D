using UnityEngine;

public class RandomItemSpawner : MonoBehaviour
{
    [SerializeField] private float minX = -15f;
    [SerializeField] private float maxX = 15f;
    [SerializeField] private float minZ = -10f;
    [SerializeField] private float maxZ = 15f;
    [SerializeField] private float spawnHeight = 0.5f;
    [SerializeField] private float minDistanceFromPlayer = 3f;
    [SerializeField] private float minDistanceBetweenItems = 5f;

    private void Start()
    {
        RandomizeItemPositions();
    }

    public void RandomizeItemPositions()
    {
        ItemPickup[] items = FindObjectsOfType<ItemPickup>();
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;

        foreach (ItemPickup item in items)
        {
            Vector3 randomPos;
            int attempts = 0;

            do
            {
                randomPos = new Vector3(
                    Random.Range(minX, maxX),
                    spawnHeight,
                    Random.Range(minZ, maxZ)
                );
                attempts++;
            }
            while ((!IsValidPosition(randomPos, player, items, item)) && attempts < 10);

            item.transform.position = randomPos;
        }
    }

    private bool IsValidPosition(Vector3 pos, Transform player, ItemPickup[] allItems, ItemPickup currentItem)
    {
        if (player != null)
        {
            float distToPlayer = Vector3.Distance(pos, player.position);
            if (distToPlayer < minDistanceFromPlayer)
                return false;
        }

        foreach (ItemPickup item in allItems)
        {
            if (item == currentItem) continue;
            float distToItem = Vector3.Distance(pos, item.transform.position);
            if (distToItem < minDistanceBetweenItems)
                return false;
        }

        return true;
    }
}

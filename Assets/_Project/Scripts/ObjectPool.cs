using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefabToPool;
    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> availableObjects = new Queue<GameObject>();
    private HashSet<GameObject> activeObjects = new HashSet<GameObject>();

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefabToPool);
            obj.SetActive(false);
            obj.name = $"{prefabToPool.name} (Pool)";
            availableObjects.Enqueue(obj);
        }

        Debug.Log($"Pool inicializado con {poolSize} objetos de {prefabToPool.name}");
    }

    public GameObject GetObject()
    {
        GameObject obj;

        if (availableObjects.Count > 0)
        {
            obj = availableObjects.Dequeue();
        }
        else
        {
            obj = Instantiate(prefabToPool);
            obj.name = $"{prefabToPool.name} (Pool Extra)";
        }

        obj.SetActive(true);
        activeObjects.Add(obj);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        if (!activeObjects.Contains(obj)) return;

        activeObjects.Remove(obj);
        obj.SetActive(false);
        availableObjects.Enqueue(obj);
    }

    public int GetAvailableCount() => availableObjects.Count;
    public int GetActiveCount() => activeObjects.Count;
}

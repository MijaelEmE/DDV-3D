using UnityEngine;
using UnityEngine.UI;

public class UIDebugger : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("=== UI DEBUGGER ===");

        GameUI gameUI = FindFirstObjectByType<GameUI>();
        if (gameUI == null)
        {
            Debug.LogError("GameUI no encontrado!");
            return;
        }

        Debug.Log($"GameUI encontrado: {gameUI.gameObject.name}");

        // Verificar PlayerInventory
        PlayerInventory inventory = FindFirstObjectByType<PlayerInventory>();
        if (inventory == null)
            Debug.LogError("PlayerInventory NO encontrado");
        else
            Debug.Log($"PlayerInventory encontrado: {inventory.gameObject.name}");

        // Verificar Canvas
        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null)
            Debug.LogError("Canvas NO encontrado");
        else
            Debug.Log($"Canvas encontrado: {canvas.gameObject.name}");

        // Buscar texts manualmente
        Text[] allTexts = FindObjectsByType<Text>(FindObjectsSortMode.None);
        Debug.Log($"Total Text components encontrados: {allTexts.Length}");

        foreach (Text text in allTexts)
        {
            Debug.Log($"  - {text.gameObject.name}: '{text.text}'");
        }

        // Verificar referencias en GameUI via reflection
        var field = gameUI.GetType().GetField("itemCountText",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (field != null)
        {
            object value = field.GetValue(gameUI);
            if (value == null)
                Debug.LogError("itemCountText NO está asignado en GameUI!");
            else
                Debug.Log($"itemCountText asignado: {value}");
        }
    }
}

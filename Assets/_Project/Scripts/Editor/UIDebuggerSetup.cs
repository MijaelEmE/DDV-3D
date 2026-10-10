using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;

public class UIDebuggerSetup
{
    [MenuItem("Doctocliq/Debug - Verificar UI")]
    public static void SetupUIDebugger()
    {
        Debug.Log("=== VERIFICACION DE UI ===\n");

        // 1. Verificar GameUI
        GameUI gameUI = GameObject.FindFirstObjectByType<GameUI>();
        if (gameUI == null)
        {
            Debug.LogError("ERROR: GameUI no encontrado en la escena!");
        }
        else
        {
            Debug.Log($"✓ GameUI encontrado: {gameUI.gameObject.name}");
        }

        // 2. Verificar PlayerInventory
        PlayerInventory inventory = GameObject.FindFirstObjectByType<PlayerInventory>();
        if (inventory == null)
        {
            Debug.LogError("ERROR: PlayerInventory no encontrado!");
        }
        else
        {
            Debug.Log($"✓ PlayerInventory encontrado: {inventory.gameObject.name}");
        }

        // 3. Verificar Canvas
        Canvas canvas = GameObject.FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("ERROR: Canvas no encontrado!");
        }
        else
        {
            Debug.Log($"✓ Canvas encontrado: {canvas.gameObject.name}");
        }

        // 4. Buscar todos los Text components
        Text[] allTexts = GameObject.FindObjectsByType<Text>(FindObjectsSortMode.None);
        Debug.Log($"\n✓ Total Text components: {allTexts.Length}");

        foreach (Text text in allTexts)
        {
            Debug.Log($"  - {text.gameObject.name}: '{text.text}'");
        }

        // 5. Verificar referencias en GameUI
        Debug.Log("\n=== REFERENCIAS EN GAMEUI ===");
        VerifyGameUIReferences(gameUI);

        // 6. Verificar referencias en GameOverUI
        Debug.Log("\n=== REFERENCIAS EN GAMEOVERUI ===");
        GameOverUI gameOverUI = GameObject.FindFirstObjectByType<GameOverUI>();
        VerifyGameOverUIReferences(gameOverUI);

        Debug.Log("\n=== VERIFICACION COMPLETADA ===\n");
    }

    private static void VerifyGameUIReferences(GameUI gameUI)
    {
        if (gameUI == null)
        {
            Debug.LogError("GameUI es null!");
            return;
        }

        var itemCountField = gameUI.GetType().GetField("itemCountText",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        var healthTextField = gameUI.GetType().GetField("healthText",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        var objectiveField = gameUI.GetType().GetField("objectiveText",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (itemCountField != null)
        {
            object value = itemCountField.GetValue(gameUI);
            if (value == null)
                Debug.LogError("✗ itemCountText NO está asignado!");
            else
                Debug.Log($"✓ itemCountText: {value}");
        }

        if (healthTextField != null)
        {
            object value = healthTextField.GetValue(gameUI);
            if (value == null)
                Debug.LogError("✗ healthText NO está asignado!");
            else
                Debug.Log($"✓ healthText: {value}");
        }

        if (objectiveField != null)
        {
            object value = objectiveField.GetValue(gameUI);
            if (value == null)
                Debug.LogError("✗ objectiveText NO está asignado!");
            else
                Debug.Log($"✓ objectiveText: {value}");
        }
    }

    private static void VerifyGameOverUIReferences(GameOverUI gameOverUI)
    {
        if (gameOverUI == null)
        {
            Debug.LogWarning("GameOverUI no encontrado");
            return;
        }

        var victoryPanelField = gameOverUI.GetType().GetField("victoryPanel",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        var defeatPanelField = gameOverUI.GetType().GetField("defeatPanel",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (victoryPanelField != null)
        {
            object value = victoryPanelField.GetValue(gameOverUI);
            if (value == null)
                Debug.LogWarning("⚠ victoryPanel NO está asignado");
            else
                Debug.Log($"✓ victoryPanel: {value}");
        }

        if (defeatPanelField != null)
        {
            object value = defeatPanelField.GetValue(gameOverUI);
            if (value == null)
                Debug.LogWarning("⚠ defeatPanel NO está asignado");
            else
                Debug.Log($"✓ defeatPanel: {value}");
        }
    }
}

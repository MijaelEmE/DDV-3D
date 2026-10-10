using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class FixGameUISetup
{
    [MenuItem("Doctocliq/Fix - Agregar GameUI al Canvas")]
    public static void FixGameUI()
    {
        Debug.Log("Agregando GameUI al Canvas...");

        // Buscar Canvas
        Canvas canvas = GameObject.FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas no encontrado!");
            return;
        }

        Debug.Log($"Canvas encontrado: {canvas.gameObject.name}");

        // Si GameUI ya existe en Canvas, removerlo
        GameUI existingGameUI = canvas.GetComponent<GameUI>();
        if (existingGameUI != null)
        {
            Debug.LogWarning("GameUI ya existe en Canvas, removiendo...");
            Object.DestroyImmediate(existingGameUI);
        }

        // Agregar GameUI al Canvas
        GameUI gameUI = canvas.gameObject.AddComponent<GameUI>();
        Debug.Log("GameUI agregado al Canvas");

        // Buscar los Text components en el Canvas (pueden estar en cualquier nivel)
        Text[] allTexts = canvas.GetComponentsInChildren<Text>();
        Debug.Log($"Encontrados {allTexts.Length} Text components");

        Text itemCountText = null;
        Text healthText = null;
        Text objectiveText = null;

        // Buscar por nombre de GameObject
        foreach (Text text in allTexts)
        {
            Debug.Log($"  Encontrado: {text.gameObject.name} = '{text.text}'");

            if (text.gameObject.name.Contains("ItemCount"))
                itemCountText = text;
            else if (text.gameObject.name.Contains("Health"))
                healthText = text;
            else if (text.gameObject.name.Contains("Objective"))
                objectiveText = text;
        }

        // Si no encontró por nombre, usar el primero, segundo, tercero
        if (itemCountText == null && allTexts.Length > 0)
            itemCountText = allTexts[0];
        if (healthText == null && allTexts.Length > 1)
            healthText = allTexts[1];
        if (objectiveText == null && allTexts.Length > 2)
            objectiveText = allTexts[2];

        // Asignar via SerializedObject
        SerializedObject so = new SerializedObject(gameUI);

        if (itemCountText != null)
        {
            so.FindProperty("itemCountText").objectReferenceValue = itemCountText;
            Debug.Log($"✓ itemCountText asignado: {itemCountText.gameObject.name}");
        }
        else
            Debug.LogError("✗ itemCountText no encontrado");

        if (healthText != null)
        {
            so.FindProperty("healthText").objectReferenceValue = healthText;
            Debug.Log($"✓ healthText asignado: {healthText.gameObject.name}");
        }
        else
            Debug.LogError("✗ healthText no encontrado");

        if (objectiveText != null)
        {
            so.FindProperty("objectiveText").objectReferenceValue = objectiveText;
            Debug.Log($"✓ objectiveText asignado: {objectiveText.gameObject.name}");
        }
        else
            Debug.LogError("✗ objectiveText no encontrado");

        so.ApplyModifiedProperties();

        Debug.Log("✓ GameUI configurado correctamente!");
    }
}

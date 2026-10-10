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

        // Buscar los Text components en el Canvas
        Transform hudTransform = canvas.transform.Find("HUD");
        if (hudTransform == null)
        {
            Debug.LogError("HUD panel no encontrado en Canvas!");
            return;
        }

        // Asignar referencias
        Text itemCountText = hudTransform.Find("ItemCount")?.GetComponent<Text>();
        Text healthText = hudTransform.Find("Health")?.GetComponent<Text>();
        Text objectiveText = hudTransform.Find("Objective")?.GetComponent<Text>();

        // Asignar via SerializedObject
        SerializedObject so = new SerializedObject(gameUI);

        if (itemCountText != null)
        {
            so.FindProperty("itemCountText").objectReferenceValue = itemCountText;
            Debug.Log("itemCountText asignado");
        }
        else
            Debug.LogWarning("itemCountText no encontrado");

        if (healthText != null)
        {
            so.FindProperty("healthText").objectReferenceValue = healthText;
            Debug.Log("healthText asignado");
        }
        else
            Debug.LogWarning("healthText no encontrado");

        if (objectiveText != null)
        {
            so.FindProperty("objectiveText").objectReferenceValue = objectiveText;
            Debug.Log("objectiveText asignado");
        }
        else
            Debug.LogWarning("objectiveText no encontrado");

        so.ApplyModifiedProperties();

        Debug.Log("✓ GameUI configurado correctamente!");
    }
}

using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class CleanDuplicateUITexts
{
    [MenuItem("GAMEDEV/1 - Limpiar UI Duplicados")]
    public static void CleanDuplicates()
    {
        Debug.Log("Limpiando duplicados de UI...");

        Canvas canvas = GameObject.FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas no encontrado!");
            return;
        }

        Text[] allTexts = canvas.GetComponentsInChildren<Text>();
        Debug.Log($"Encontrados {allTexts.Length} Text components");

        // Diccionario para rastrear qué textos mantener
        System.Collections.Generic.Dictionary<string, Text> textsByName = new System.Collections.Generic.Dictionary<string, Text>();
        System.Collections.Generic.List<GameObject> toDelete = new System.Collections.Generic.List<GameObject>();

        foreach (Text text in allTexts)
        {
            string key = text.gameObject.name;
            Debug.Log($"  Encontrado: {key} = '{text.text}'");

            if (textsByName.ContainsKey(key))
            {
                // Es un duplicado, marcarlo para eliminar
                Debug.LogWarning($"  DUPLICADO encontrado: {key}, eliminando...");
                toDelete.Add(text.gameObject);
            }
            else
            {
                textsByName[key] = text;
            }
        }

        // Eliminar duplicados
        foreach (GameObject obj in toDelete)
        {
            Debug.Log($"Eliminando: {obj.name}");
            Object.DestroyImmediate(obj);
        }

        // Verificar que solo haya los 3 textos principales
        Text[] finalTexts = canvas.GetComponentsInChildren<Text>();
        Debug.Log($"\nTexts finales: {finalTexts.Length}");
        foreach (Text text in finalTexts)
        {
            Debug.Log($"  ✓ {text.gameObject.name}: '{text.text}'");
        }

        Debug.Log("✓ Limpieza completada!");
    }
}

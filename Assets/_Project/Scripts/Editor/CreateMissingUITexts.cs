using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class CreateMissingUITexts
{
    [MenuItem("Doctocliq/Fix - Crear Texts Faltantes")]
    public static void CreateMissingTexts()
    {
        Debug.Log("Creando texts faltantes...");

        Canvas canvas = GameObject.FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas no encontrado!");
            return;
        }

        // Buscar si ItemCount existe
        Transform itemCountTrans = canvas.transform.Find("ItemCount");
        if (itemCountTrans == null)
        {
            Debug.LogWarning("ItemCount no encontrado, creando...");
            CreateText(canvas, "ItemCount", "Items: 0/3", 20, -20);
        }
        else
            Debug.Log("✓ ItemCount ya existe");

        // Buscar si Health existe
        Transform healthTrans = canvas.transform.Find("Health");
        if (healthTrans == null)
        {
            Debug.Log("Creando Health...");
            CreateText(canvas, "Health", "Vidas: 3", 20, -50);
        }
        else
            Debug.Log("✓ Health ya existe");

        // Buscar si Objective existe
        Transform objectiveTrans = canvas.transform.Find("Objective");
        if (objectiveTrans == null)
        {
            Debug.Log("Creando Objective...");
            CreateText(canvas, "Objective", "Encuentra 3 objetos", 20, -80);
        }
        else
            Debug.Log("✓ Objective ya existe");

        Debug.Log("✓ Texts completados!");
    }

    private static void CreateText(Canvas canvas, string name, string text, float x, float y)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(canvas.transform);

        Text txt = textObj.AddComponent<Text>();
        txt.text = text;
        txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        txt.fontSize = 30;
        txt.fontStyle = FontStyle.Normal;
        txt.color = Color.white;
        txt.alignment = TextAnchor.MiddleCenter;

        RectTransform rect = textObj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(x, y);
        rect.sizeDelta = new Vector2(400, 100);

        Debug.Log($"✓ {name} creado en posición ({x}, {y})");
    }
}

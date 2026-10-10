using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class CreateMissingUITexts
{
    [MenuItem("GAMEDEV/2 - Crear Texts Faltantes")]
    public static void CreateMissingTexts()
    {
        Debug.Log("Creando texts faltantes...");

        Canvas canvas = GameObject.FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas no encontrado!");
            return;
        }

        // ABAJO A LA IZQUIERDA: Items
        Transform itemCountTrans = canvas.transform.Find("ItemCount");
        if (itemCountTrans == null)
        {
            CreateText(canvas, "ItemCount", "Items: 0/3", -100, -50, TextAnchor.LowerLeft);
        }
        else
        {
            RectTransform rect = itemCountTrans.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(-100, -50);
            Text txt = itemCountTrans.GetComponent<Text>();
            txt.alignment = TextAnchor.LowerLeft;
            Debug.Log("✓ ItemCount reposicionado (abajo izquierda)");
        }

        // ABAJO A LA DERECHA: Vidas
        Transform healthTrans = canvas.transform.Find("Health");
        if (healthTrans == null)
        {
            CreateText(canvas, "Health", "Vidas: 3", 100, -50, TextAnchor.LowerRight);
        }
        else
        {
            RectTransform rect = healthTrans.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(100, -50);
            Text txt = healthTrans.GetComponent<Text>();
            txt.alignment = TextAnchor.LowerRight;
            Debug.Log("✓ Health reposicionado (abajo derecha)");
        }

        // ARRIBA EN EL CENTRO: Objetivo
        Transform objectiveTrans = canvas.transform.Find("Objective");
        if (objectiveTrans == null)
        {
            CreateText(canvas, "Objective", "Encuentra 3 objetos", 0, 50, TextAnchor.UpperCenter);
        }
        else
        {
            RectTransform rect = objectiveTrans.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0, 50);
            Text txt = objectiveTrans.GetComponent<Text>();
            txt.alignment = TextAnchor.UpperCenter;
            Debug.Log("✓ Objective reposicionado (arriba centro)");
        }

        Debug.Log("✓ Texts posicionados correctamente!");
    }

    private static void CreateText(Canvas canvas, string name, string text, float x, float y, TextAnchor alignment)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(canvas.transform);

        Text txt = textObj.AddComponent<Text>();
        txt.text = text;
        txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        txt.fontSize = 30;
        txt.fontStyle = FontStyle.Normal;
        txt.color = Color.white;
        txt.alignment = alignment;

        RectTransform rect = textObj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(x, y);
        rect.sizeDelta = new Vector2(400, 100);

        Debug.Log($"✓ {name} creado en posición ({x}, {y})");
    }
}

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class Phase3Setup
{
    [MenuItem("Doctocliq/Setup Phase 3 - Audio UI VFX")]
    public static void SetupPhase3()
    {
        Debug.Log("Iniciando Setup Fase 3...");

        try
        {
            CreateAudioManager();
            CreateCanvasUI();

            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            Debug.Log("Setup Fase 3 completado. UI y Audio agregados.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error: {ex.Message}\n{ex.StackTrace}");
        }
    }

    private static void CreateAudioManager()
    {
        if (GameObject.Find("AudioManager") != null)
        {
            Debug.LogWarning("AudioManager ya existe");
            return;
        }

        GameObject audioObj = new GameObject("AudioManager");
        audioObj.AddComponent<AudioManager>();
        Debug.Log("AudioManager creado");
    }

    private static void CreateCanvasUI()
    {
        if (GameObject.Find("Canvas") != null)
        {
            Debug.LogWarning("Canvas ya existe");
            return;
        }

        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        GraphicRaycaster raycaster = canvasObj.AddComponent<GraphicRaycaster>();

        RectTransform canvasRect = canvasObj.GetComponent<RectTransform>();
        canvasRect.anchorMin = Vector2.zero;
        canvasRect.anchorMax = Vector2.one;
        canvasRect.offsetMin = Vector2.zero;
        canvasRect.offsetMax = Vector2.zero;

        // HUD Panel
        GameObject hudObj = CreatePanel(canvasObj, "HUD", new Color(0, 0, 0, 0));
        CreateText(hudObj, "ItemCount", "Items: 0/3", Vector2.up, new Vector2(-100, -20));
        CreateText(hudObj, "Health", "Vidas: 3", Vector2.up, new Vector2(-100, -50));
        CreateText(hudObj, "Objective", "Encuentra 3 objetos", Vector2.up, new Vector2(-100, -80));

        // Victory Panel
        GameObject victoryObj = CreatePanel(canvasObj, "VictoryPanel", new Color(0, 0, 0, 0.8f));
        CreateText(victoryObj, "VictoryText", "VICTORIA!\nEscapaste del templo", Vector2.zero, Vector2.zero, 60);
        CreateButton(victoryObj, "RestartButton", "Reintentar", Vector2.zero, new Vector2(0, -80));

        // Defeat Panel
        GameObject defeatObj = CreatePanel(canvasObj, "DefeatPanel", new Color(0, 0, 0, 0.8f));
        CreateText(defeatObj, "DefeatText", "DERROTA!\nEl guardian te capturo", Vector2.zero, Vector2.zero, 60);
        CreateButton(defeatObj, "RestartButton", "Reintentar", Vector2.zero, new Vector2(0, -80));

        canvasObj.AddComponent<GameUI>();
        canvasObj.AddComponent<GameOverUI>();

        Debug.Log("Canvas y UI creados");
    }

    private static GameObject CreatePanel(GameObject parent, string name, Color color)
    {
        GameObject panelObj = new GameObject(name);
        panelObj.transform.SetParent(parent.transform);

        Image image = panelObj.AddComponent<Image>();
        image.color = color;

        RectTransform rect = panelObj.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        return panelObj;
    }

    private static void CreateText(GameObject parent, string name, string text, Vector2 anchor, Vector2 pos, int fontSize = 30)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(parent.transform);

        TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.color = Color.white;
        tmp.alignment = TextAlignmentOptions.TopLeft;

        RectTransform rect = textObj.GetComponent<RectTransform>();
        rect.anchoredPosition = pos;
        rect.sizeDelta = new Vector2(300, 100);
    }

    private static void CreateButton(GameObject parent, string name, string buttonText, Vector2 anchor, Vector2 pos)
    {
        GameObject buttonObj = new GameObject(name);
        buttonObj.transform.SetParent(parent.transform);

        Image image = buttonObj.AddComponent<Image>();
        image.color = new Color(0.2f, 0.6f, 1f);

        Button button = buttonObj.AddComponent<Button>();

        RectTransform rect = buttonObj.GetComponent<RectTransform>();
        rect.anchoredPosition = pos;
        rect.sizeDelta = new Vector2(200, 60);

        GameObject textChild = new GameObject("Text");
        textChild.transform.SetParent(buttonObj.transform);

        TextMeshProUGUI tmp = textChild.AddComponent<TextMeshProUGUI>();
        tmp.text = buttonText;
        tmp.fontSize = 36;
        tmp.color = Color.white;
        tmp.alignment = TextAlignmentOptions.Center;

        RectTransform textRect = textChild.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;
    }
}

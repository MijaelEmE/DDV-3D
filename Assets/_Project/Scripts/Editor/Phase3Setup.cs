using UnityEngine;
using UnityEngine.UI;
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

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        GraphicRaycaster raycaster = canvasObj.AddComponent<GraphicRaycaster>();

        RectTransform canvasRect = canvasObj.GetComponent<RectTransform>();
        canvasRect.anchorMin = Vector2.zero;
        canvasRect.anchorMax = Vector2.one;
        canvasRect.offsetMin = Vector2.zero;
        canvasRect.offsetMax = Vector2.zero;

        CreateHUDPanel(canvasObj);
        CreateVictoryPanel(canvasObj);
        CreateDefeatPanel(canvasObj);

        canvasObj.AddComponent<GameUI>();
        canvasObj.AddComponent<GameOverUI>();

        Debug.Log("Canvas y UI creados");
    }

    private static void CreateHUDPanel(GameObject canvas)
    {
        GameObject hudObj = new GameObject("HUD");
        hudObj.transform.SetParent(canvas.transform);
        RectTransform hudRect = hudObj.AddComponent<RectTransform>();
        hudRect.anchoredPosition = Vector2.zero;

        CreateSimpleText(hudObj, "ItemCount", "Items: 0/3", 20, -20);
        CreateSimpleText(hudObj, "Health", "Vidas: 3", 20, -50);
        CreateSimpleText(hudObj, "Objective", "Encuentra 3 objetos", 20, -80);
    }

    private static void CreateVictoryPanel(GameObject canvas)
    {
        GameObject panel = new GameObject("VictoryPanel");
        panel.transform.SetParent(canvas.transform);

        Image image = panel.AddComponent<Image>();
        image.color = new Color(0, 0, 0, 0.7f);

        RectTransform rect = panel.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        CreateSimpleText(panel, "Text", "VICTORIA!\nEscapaste!", 0, 50, 40);
        CreateSimpleButton(panel, "RestartButton", "Reintentar", 0, -80);
    }

    private static void CreateDefeatPanel(GameObject canvas)
    {
        GameObject panel = new GameObject("DefeatPanel");
        panel.transform.SetParent(canvas.transform);

        Image image = panel.AddComponent<Image>();
        image.color = new Color(0, 0, 0, 0.7f);

        RectTransform rect = panel.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        CreateSimpleText(panel, "Text", "DERROTA!\nEl guardian gano", 0, 50, 40);
        CreateSimpleButton(panel, "RestartButton", "Reintentar", 0, -80);
    }

    private static void CreateSimpleText(GameObject parent, string name, string text, float x, float y, int fontSize = 30)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(parent.transform);

        Text txt = textObj.AddComponent<Text>();
        txt.text = text;
        txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        txt.fontSize = fontSize;
        txt.fontStyle = FontStyle.Normal;
        txt.color = Color.white;
        txt.alignment = TextAnchor.MiddleCenter;

        RectTransform rect = textObj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(x, y);
        rect.sizeDelta = new Vector2(400, 100);
    }

    private static void CreateSimpleButton(GameObject parent, string name, string text, float x, float y)
    {
        GameObject buttonObj = new GameObject(name);
        buttonObj.transform.SetParent(parent.transform);

        Image image = buttonObj.AddComponent<Image>();
        image.color = new Color(0.2f, 0.6f, 1f);

        Button button = buttonObj.AddComponent<Button>();
        button.targetGraphic = image;

        RectTransform rect = buttonObj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(x, y);
        rect.sizeDelta = new Vector2(200, 60);

        CreateSimpleText(buttonObj, "Text", text, 0, 0, 28);
    }
}

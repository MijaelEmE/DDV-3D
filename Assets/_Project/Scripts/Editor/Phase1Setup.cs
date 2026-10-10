using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class Phase1Setup
{
    [MenuItem("Doctocliq/Setup Phase 1 - Core Gameplay")]
    public static void SetupPhase1()
    {
        Debug.Log("🚀 Iniciando Setup Fase 1...");

        try
        {
            CreatePlayerInventory();
            CreateGameManager();
            CreateObjectiveManager();
            CreateRandomItemSpawner();
            CreateSampleItems();
            ConfigurePortal();
            ImproveAltars();

            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            Debug.Log("Setup Fase 1 completado. Items posicionados aleatoriamente.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"❌ Error: {ex.Message}\n{ex.StackTrace}");
        }
    }

    private static void CreatePlayerInventory()
    {
        if (GameObject.Find("PlayerInventory") != null)
        {
            Debug.LogWarning("⚠️ PlayerInventory ya existe");
            return;
        }

        GameObject obj = new GameObject("PlayerInventory");
        obj.AddComponent<PlayerInventory>();
        Debug.Log("✅ PlayerInventory creado");
    }

    private static void CreateGameManager()
    {
        if (GameObject.Find("GameManager") != null)
        {
            Debug.LogWarning("⚠️ GameManager ya existe");
            return;
        }

        GameObject obj = new GameObject("GameManager");
        obj.AddComponent<GameManager>();
        Debug.Log("✅ GameManager creado");
    }

    private static void CreateObjectiveManager()
    {
        if (GameObject.Find("ObjectiveManager") != null)
        {
            Debug.LogWarning("⚠️ ObjectiveManager ya existe");
            return;
        }

        GameObject obj = new GameObject("ObjectiveManager");
        ObjectiveManager manager = obj.AddComponent<ObjectiveManager>();

        // Crear portal si no existe
        GameObject portal = GameObject.Find("Portal");
        if (portal == null)
        {
            portal = new GameObject("Portal");
            portal.transform.position = new Vector3(0, 1, 10);

            GameObject model = GameObject.CreatePrimitive(PrimitiveType.Cube);
            model.name = "PortalModel";
            model.transform.SetParent(portal.transform);
            model.transform.localScale = new Vector3(2, 2, 0.1f);
            model.transform.localPosition = Vector3.zero;

            Light light = portal.AddComponent<Light>();
            light.type = LightType.Point;
            light.intensity = 2;
            light.range = 10;
            light.color = new Color(0.5f, 0.8f, 1f);
            light.enabled = false;

            SphereCollider col = portal.AddComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = 2;

            portal.AddComponent<PortalExit>();
            Debug.Log("✅ Portal creado");
        }

        Debug.Log("✅ ObjectiveManager creado");
    }

    private static void CreateSampleItems()
    {
        if (GameObject.Find("AncientKey") != null)
        {
            Debug.LogWarning("⚠️ Items ya existen");
            return;
        }

        string[] names = { "AncientKey", "MysticOrb", "SacredAmulet" };
        Vector3[] positions = {
            new Vector3(-5, 0.5f, 0),
            new Vector3(5, 0.5f, 0),
            new Vector3(0, 0.5f, 5)
        };
        Color[] colors = {
            new Color(1, 0.84f, 0),
            new Color(0.5f, 0, 1),
            new Color(1, 0, 0.5f)
        };

        for (int i = 0; i < names.Length; i++)
        {
            GameObject item = GameObject.CreatePrimitive(PrimitiveType.Cube);
            item.name = names[i];
            item.transform.position = positions[i];
            item.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            Collider col = item.GetComponent<Collider>();
            col.isTrigger = true;

            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null) Object.DestroyImmediate(rb);

            Renderer renderer = item.GetComponent<Renderer>();
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = colors[i];
            renderer.material = mat;

            ItemPickup pickup = item.AddComponent<ItemPickup>();
            Debug.Log($"✅ Item creado: {names[i]}");
        }
    }

    private static void ConfigurePortal()
    {
        GameObject portal = GameObject.Find("Portal");
        if (portal == null) return;

        if (portal.GetComponent<PortalExit>() == null)
            portal.AddComponent<PortalExit>();

        Debug.Log("✅ Portal configurado");
    }

    private static void ImproveAltars()
    {
        AltarInteractable[] altars = GameObject.FindObjectsOfType<AltarInteractable>();

        foreach (AltarInteractable altar in altars)
        {
            if (altar.GetComponent<AltarActivationEffects>() == null)
            {
                AltarActivationEffects effects = altar.gameObject.AddComponent<AltarActivationEffects>();
                Debug.Log($"Altar mejorado: {altar.gameObject.name}");
            }
        }
    }

    private static void CreateRandomItemSpawner()
    {
        if (GameObject.Find("RandomItemSpawner") != null)
            return;

        GameObject spawnerObj = new GameObject("RandomItemSpawner");
        RandomItemSpawner spawner = spawnerObj.AddComponent<RandomItemSpawner>();
        Debug.Log("RandomItemSpawner creado");
    }
}

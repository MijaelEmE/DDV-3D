using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class Phase1Setup
{
    [MenuItem("Doctocliq/Setup Phase 1 - Core Gameplay")]
    public static void SetupPhase1()
    {
        Debug.Log("🚀 Iniciando Setup Fase 1...");

        string scenePath = "Assets/_Project/Scenes/Main.unity";
        var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);

        try
        {
            // 1. Crear PlayerInventory
            CreatePlayerInventory();

            // 2. Crear GameManager
            CreateGameManager();

            // 3. Crear ObjectiveManager
            CreateObjectiveManager();

            // 4. Crear Items de Ejemplo
            CreateSampleItems();

            // 5. Configurar Portal si existe
            ConfigurePortal();

            // 6. Mejorar Altares
            ImproveAltars();

            EditorSceneManager.SaveScene(scene);
            Debug.Log("✅ Setup Fase 1 completado. Verifica la escena en el Editor.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"❌ Error durante setup: {ex.Message}\n{ex.StackTrace}");
        }
    }

    private static void CreatePlayerInventory()
    {
        if (GameObject.Find("PlayerInventory") != null)
        {
            Debug.LogWarning("⚠️ PlayerInventory ya existe");
            return;
        }

        GameObject playerInvObj = new GameObject("PlayerInventory");
        PlayerInventory inventory = playerInvObj.AddComponent<PlayerInventory>();

        // Configurar propiedades via serialización
        var so = new SerializedObject(inventory);
        so.FindProperty("maxItems").intValue = 3;
        so.ApplyModifiedProperties();

        Debug.Log("✅ PlayerInventory creado");
    }

    private static void CreateGameManager()
    {
        if (GameObject.Find("GameManager") != null)
        {
            Debug.LogWarning("⚠️ GameManager ya existe");
            return;
        }

        GameObject gameManagerObj = new GameObject("GameManager");
        GameManager gameManager = gameManagerObj.AddComponent<GameManager>();

        Debug.Log("✅ GameManager creado");
    }

    private static void CreateObjectiveManager()
    {
        if (GameObject.Find("ObjectiveManager") != null)
        {
            Debug.LogWarning("⚠️ ObjectiveManager ya existe");
            return;
        }

        GameObject objectiveObj = new GameObject("ObjectiveManager");
        ObjectiveManager objective = objectiveObj.AddComponent<ObjectiveManager>();

        // Crear placeholder portal si no existe
        GameObject portal = GameObject.Find("Portal");
        if (portal == null)
        {
            portal = new GameObject("Portal");
            portal.transform.position = new Vector3(0, 1, 10);

            // Agregar geometría
            GameObject portalModel = GameObject.CreatePrimitive(PrimitiveType.Cube);
            portalModel.name = "PortalModel";
            portalModel.transform.SetParent(portal.transform);
            portalModel.transform.localScale = new Vector3(2, 2, 0.1f);
            portalModel.transform.localPosition = Vector3.zero;

            // Agregar luz
            Light portalLight = portal.AddComponent<Light>();
            portalLight.type = LightType.Point;
            portalLight.intensity = 2;
            portalLight.range = 10;
            portalLight.color = new Color(0.5f, 0.8f, 1f); // Azul cian
            portalLight.enabled = false;

            // Agregar trigger
            SphereCollider trigger = portal.AddComponent<SphereCollider>();
            trigger.isTrigger = true;
            trigger.radius = 2;

            portal.AddComponent<PortalExit>();

            Debug.Log("✅ Portal creado (placeholder)");
        }

        // Configurar ObjectiveManager
        var so = new SerializedObject(objective);
        so.FindProperty("portalObject").objectReferenceValue = portal;
        var portalLight = portal.GetComponent<Light>();
        so.FindProperty("portalLight").objectReferenceValue = portalLight;
        so.FindProperty("objectiveText").stringValue = "Encuentra 3 objetos antiguos";
        so.ApplyModifiedProperties();

        Debug.Log("✅ ObjectiveManager creado");
    }

    private static void CreateSampleItems()
    {
        // Verificar si ya existen
        if (GameObject.Find("AncientKey") != null)
        {
            Debug.LogWarning("⚠️ Items ya existen");
            return;
        }

        // Tag "Player" si no existe
        int playerLayer = LayerMask.NameToLayer("Default");

        string[] itemNames = { "AncientKey", "MysticOrb", "SacredAmulet" };
        Vector3[] itemPositions = {
            new Vector3(-5, 0.5f, 0),
            new Vector3(5, 0.5f, 0),
            new Vector3(0, 0.5f, 5)
        };
        Color[] itemColors = {
            new Color(1, 0.84f, 0), // Gold
            new Color(0.5f, 0, 1),  // Purple
            new Color(1, 0, 0.5f)   // Pink
        };

        for (int i = 0; i < itemNames.Length; i++)
        {
            GameObject item = GameObject.CreatePrimitive(PrimitiveType.Cube);
            item.name = itemNames[i];
            item.transform.position = itemPositions[i];
            item.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            // Configurar collider como trigger
            Collider col = item.GetComponent<Collider>();
            col.isTrigger = true;

            // Quitar Rigidbody
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null) Object.DestroyImmediate(rb);

            // Colorear
            Renderer renderer = item.GetComponent<Renderer>();
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = itemColors[i];
            renderer.material = mat;

            // Agregar ItemPickup
            ItemPickup pickup = item.AddComponent<ItemPickup>();
            var so = new SerializedObject(pickup);
            so.FindProperty("itemName").stringValue = itemNames[i];
            so.ApplyModifiedProperties();

            Debug.Log($"✅ Item creado: {itemNames[i]}");
        }
    }

    private static void ConfigurePortal()
    {
        GameObject portal = GameObject.Find("Portal");

        if (portal == null)
        {
            Debug.LogWarning("⚠️ Portal no encontrado, se debería haber creado");
            return;
        }

        // Asegurar que tiene PortalExit
        if (portal.GetComponent<PortalExit>() == null)
            portal.AddComponent<PortalExit>();

        // Asegurar que tiene trigger
        if (portal.GetComponent<Collider>() == null)
        {
            SphereCollider trigger = portal.AddComponent<SphereCollider>();
            trigger.isTrigger = true;
            trigger.radius = 2;
        }
        else
        {
            Collider col = portal.GetComponent<Collider>();
            col.isTrigger = true;
        }

        Debug.Log("✅ Portal configurado");
    }

    private static void ImproveAltars()
    {
        // Buscar todos los altares
        AltarInteractable[] altars = GameObject.FindObjectsOfType<AltarInteractable>();

        foreach (AltarInteractable altar in altars)
        {
            // Asegurar que tiene collider como trigger
            Collider col = altar.GetComponent<Collider>();
            if (col != null)
                col.isTrigger = true;

            // Agregar AltarActivationEffects si no existe
            if (altar.GetComponent<AltarActivationEffects>() == null)
            {
                AltarActivationEffects effects = altar.gameObject.AddComponent<AltarActivationEffects>();

                var so = new SerializedObject(effects);
                so.FindProperty("altar").objectReferenceValue = altar;

                // Buscar o crear luz
                Light light = altar.GetComponent<Light>();
                if (light == null)
                {
                    light = altar.gameObject.AddComponent<Light>();
                    light.type = LightType.Point;
                    light.intensity = 1.5f;
                    light.range = 5;
                    light.enabled = false;
                }
                so.FindProperty("altarLight").objectReferenceValue = light;

                so.ApplyModifiedProperties();

                Debug.Log($"✅ Altar mejorado: {altar.gameObject.name}");
            }
        }

        if (altars.Length == 0)
            Debug.LogWarning("⚠️ No se encontraron altares en la escena");
    }
}

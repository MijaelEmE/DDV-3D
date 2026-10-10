using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class Phase2Setup
{
    [MenuItem("Doctocliq/Setup Phase 2 - AI Guardian")]
    public static void SetupPhase2()
    {
        Debug.Log("🚀 Iniciando Setup Fase 2...");

        try
        {
            CreateGuardian();
            AddPlayerHealth();
            CreatePatrolPath();

            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            Debug.Log("✅ Setup Fase 2 completado. Guardian listo para jugar.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"❌ Error: {ex.Message}\n{ex.StackTrace}");
        }
    }

    private static void CreateGuardian()
    {
        if (GameObject.Find("Guardian") != null)
        {
            Debug.LogWarning("⚠️ Guardian ya existe");
            return;
        }

        // Crear guardian como cubo rojo grande
        GameObject guardian = GameObject.CreatePrimitive(PrimitiveType.Cube);
        guardian.name = "Guardian";
        guardian.transform.position = new Vector3(0, 0.5f, -5);
        guardian.transform.localScale = new Vector3(1.5f, 2f, 1.5f);

        // Configurar material (rojo)
        Renderer renderer = guardian.GetComponent<Renderer>();
        Material mat = new Material(Shader.Find("Standard"));
        mat.color = new Color(1, 0.2f, 0.2f); // Rojo
        renderer.material = mat;

        // Configurar colliders
        Collider col = guardian.GetComponent<Collider>();
        col.isTrigger = false;

        CapsuleCollider triggerCol = guardian.AddComponent<CapsuleCollider>();
        triggerCol.isTrigger = true;
        triggerCol.radius = 1f;
        triggerCol.height = 2.5f;

        // Agregar Rigidbody
        Rigidbody rb = guardian.GetComponent<Rigidbody>();
        if (rb == null) rb = guardian.AddComponent<Rigidbody>();
        rb.mass = 2f;
        rb.drag = 2f;
        rb.angularDrag = 0.5f;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Agregar scripts
        guardian.AddComponent<AIStateMachine>();
        GuardianAI ai = guardian.AddComponent<GuardianAI>();

        // Configurar AudioSource
        AudioSource audio = guardian.AddComponent<AudioSource>();
        audio.spatialBlend = 1f;

        Debug.Log("✅ Guardian creado en posición (0, 0.5, -5)");
    }

    private static void AddPlayerHealth()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("⚠️ Player no encontrado. Necesita tag 'Player'");
            return;
        }

        if (player.GetComponent<PlayerHealth>() != null)
        {
            Debug.LogWarning("⚠️ PlayerHealth ya existe");
            return;
        }

        PlayerHealth health = player.AddComponent<PlayerHealth>();

        // Agregar Rigidbody si no existe
        if (player.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = player.AddComponent<Rigidbody>();
            rb.mass = 1f;
            rb.drag = 5f;
        }

        Debug.Log("✅ PlayerHealth agregado");
    }

    private static void CreatePatrolPath()
    {
        if (GameObject.Find("PatrolPath") != null)
        {
            Debug.LogWarning("⚠️ PatrolPath ya existe");
            return;
        }

        GameObject pathObj = new GameObject("PatrolPath");
        PatrolPath path = pathObj.AddComponent<PatrolPath>();

        // Configurar waypoints (via inspector)
        // Puntos: arriba, abajo-izq, arriba-der, centro
        Debug.Log("✅ PatrolPath creado. Configura waypoints en el Inspector:");
        Debug.Log("  - Punto 0: (-10, 0, 0)");
        Debug.Log("  - Punto 1: (-10, 0, 5)");
        Debug.Log("  - Punto 2: (10, 0, 5)");
        Debug.Log("  - Punto 3: (10, 0, 0)");
    }
}

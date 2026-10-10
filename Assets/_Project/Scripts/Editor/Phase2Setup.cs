using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class Phase2Setup
{
    [MenuItem("Doctocliq/Setup Phase 2 - AI Guardian")]
    public static void SetupPhase2()
    {
        Debug.Log("Iniciando Setup Fase 2...");

        try
        {
            CreatePatrolPath();
            CreateGuardian();
            AddPlayerHealth();

            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            Debug.Log("Setup Fase 2 completado. Guardian listo para jugar.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error: {ex.Message}\n{ex.StackTrace}");
        }
    }

    private static void CreatePatrolPath()
    {
        if (GameObject.Find("PatrolPath") != null)
        {
            Debug.LogWarning("PatrolPath ya existe");
            return;
        }

        GameObject pathObj = new GameObject("PatrolPath");
        PatrolPath path = pathObj.AddComponent<PatrolPath>();

        SerializedObject so = new SerializedObject(path);
        SerializedProperty waypointsProp = so.FindProperty("waypoints");

        waypointsProp.arraySize = 4;
        waypointsProp.GetArrayElementAtIndex(0).vector3Value = new Vector3(-10, 0, 0);
        waypointsProp.GetArrayElementAtIndex(1).vector3Value = new Vector3(-10, 0, 5);
        waypointsProp.GetArrayElementAtIndex(2).vector3Value = new Vector3(10, 0, 5);
        waypointsProp.GetArrayElementAtIndex(3).vector3Value = new Vector3(10, 0, 0);

        so.ApplyModifiedProperties();
        EditorUtility.SetDirty(path);

        Debug.Log("PatrolPath creado con waypoints");
    }

    private static void CreateGuardian()
    {
        if (GameObject.Find("Guardian") != null)
        {
            Debug.LogWarning("Guardian ya existe");
            return;
        }

        GameObject guardian = GameObject.CreatePrimitive(PrimitiveType.Cube);
        guardian.name = "Guardian";
        guardian.transform.position = new Vector3(0, 0.5f, -5);
        guardian.transform.localScale = new Vector3(1.5f, 2f, 1.5f);

        Renderer renderer = guardian.GetComponent<Renderer>();
        Material mat = new Material(Shader.Find("Standard"));
        mat.color = new Color(1, 0.2f, 0.2f);
        renderer.material = mat;

        Collider col = guardian.GetComponent<Collider>();
        col.isTrigger = false;

        CapsuleCollider triggerCol = guardian.AddComponent<CapsuleCollider>();
        triggerCol.isTrigger = true;
        triggerCol.radius = 1f;
        triggerCol.height = 2.5f;

        Rigidbody rb = guardian.GetComponent<Rigidbody>();
        if (rb == null) rb = guardian.AddComponent<Rigidbody>();
        rb.mass = 2f;
        rb.drag = 2f;
        rb.angularDrag = 0.5f;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        guardian.AddComponent<AIStateMachine>();
        GuardianAI ai = guardian.AddComponent<GuardianAI>();

        AudioSource audio = guardian.AddComponent<AudioSource>();
        audio.spatialBlend = 1f;

        GameObject patrolPathObj = GameObject.Find("PatrolPath");
        if (patrolPathObj != null)
        {
            PatrolPath patrolPath = patrolPathObj.GetComponent<PatrolPath>();
            SerializedObject guardianSO = new SerializedObject(ai);
            guardianSO.FindProperty("patrolPath").objectReferenceValue = patrolPath;
            guardianSO.ApplyModifiedProperties();
        }

        Debug.Log("Guardian creado y configurado");
    }

    private static void AddPlayerHealth()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("Player no encontrado");
            return;
        }

        if (player.GetComponent<PlayerHealth>() != null)
        {
            Debug.LogWarning("PlayerHealth ya existe");
            return;
        }

        PlayerHealth health = player.AddComponent<PlayerHealth>();

        if (player.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = player.AddComponent<Rigidbody>();
            rb.mass = 1f;
            rb.drag = 5f;
        }

        Debug.Log("PlayerHealth agregado");
    }
}

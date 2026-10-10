using UnityEngine;
using UnityEditor;

public class ImproveGuardianVisuals
{
    [MenuItem("Doctocliq/Visual - Mejorar Guardian")]
    public static void ImproveGuardian()
    {
        Debug.Log("Mejorando visual del Guardian...");

        GuardianAI guardianAI = GameObject.FindFirstObjectByType<GuardianAI>();
        if (guardianAI == null)
        {
            Debug.LogError("Guardian no encontrado!");
            return;
        }

        GameObject guardian = guardianAI.gameObject;
        Debug.Log($"Guardian encontrado: {guardian.name}");

        // Remover la geometría actual (cubo)
        MeshFilter meshFilter = guardian.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            Object.DestroyImmediate(meshFilter);
            Debug.Log("Mesh removido");
        }

        // Crear una forma más interesante (cilindro para que se vea como un golem/enemigo)
        GameObject model = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        model.name = "GuardianModel";
        model.transform.SetParent(guardian.transform);
        model.transform.localPosition = Vector3.zero;
        model.transform.localScale = new Vector3(1f, 2f, 1f); // Más alto que ancho

        // Remover el collider del modelo (ya tiene uno el guardian)
        Collider modelCollider = model.GetComponent<Collider>();
        Object.DestroyImmediate(modelCollider);

        // Material rojo oscuro intimidante
        Renderer renderer = model.GetComponent<Renderer>();
        Material guardianMat = new Material(Shader.Find("Standard"));
        guardianMat.color = new Color(0.8f, 0.1f, 0.1f); // Rojo oscuro
        guardianMat.SetFloat("_Metallic", 0.3f);
        guardianMat.SetFloat("_Glossiness", 0.2f);
        renderer.material = guardianMat;

        // Agregar cabeza (esfera pequeña arriba)
        GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        head.name = "GuardianHead";
        head.transform.SetParent(guardian.transform);
        head.transform.localPosition = new Vector3(0, 1.5f, 0);
        head.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        Collider headCollider = head.GetComponent<Collider>();
        Object.DestroyImmediate(headCollider);

        Renderer headRenderer = head.GetComponent<Renderer>();
        headRenderer.material = guardianMat;

        Debug.Log("✓ Guardian visual mejorado!");
        Debug.Log("  - Cuerpo: Cilindro rojo oscuro");
        Debug.Log("  - Cabeza: Esfera roja oscura");
        Debug.Log("  - Efecto metalizado");
    }
}

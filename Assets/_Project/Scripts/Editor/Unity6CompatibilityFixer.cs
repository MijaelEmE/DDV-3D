using UnityEngine;
using UnityEditor;
using System.IO;

public class Unity6CompatibilityFixer
{
    [MenuItem("Doctocliq/Fix Unity 6 Compatibility")]
    public static void FixCompatibility()
    {
        Debug.Log("Arreglando compatibilidad con Unity 6...");

        string projectPath = "Assets/_Project/Scripts";
        string[] scriptFiles = Directory.GetFiles(projectPath, "*.cs", System.IO.SearchOption.AllDirectories);

        foreach (string filePath in scriptFiles)
        {
            if (filePath.Contains("Editor")) continue;

            string content = File.ReadAllText(filePath);
            bool changed = false;

            // Reemplazar FindObjectOfType con FindFirstObjectByType
            if (content.Contains("FindObjectOfType<"))
            {
                content = System.Text.RegularExpressions.Regex.Replace(
                    content,
                    @"FindObjectOfType<(\w+)>\(\)",
                    "FindFirstObjectByType<$1>()"
                );
                changed = true;
            }

            // Reemplazar Rigidbody.velocity con linearVelocity
            if (content.Contains("rb.velocity") || content.Contains("rigidbody.velocity"))
            {
                content = content.Replace("rb.velocity", "rb.linearVelocity");
                content = content.Replace("rigidbody.velocity", "rigidbody.linearVelocity");
                changed = true;
            }

            // Reemplazar Rigidbody.drag con linearDamping
            if (content.Contains("rb.drag"))
            {
                content = content.Replace("rb.drag", "rb.linearDamping");
                changed = true;
            }

            // Reemplazar Rigidbody.angularDrag con angularDamping
            if (content.Contains("rb.angularDrag"))
            {
                content = content.Replace("rb.angularDrag", "rb.angularDamping");
                changed = true;
            }

            if (changed)
            {
                File.WriteAllText(filePath, content);
                Debug.Log($"Arreglado: {Path.GetFileName(filePath)}");
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("Compatibilidad con Unity 6 completada!");
    }
}

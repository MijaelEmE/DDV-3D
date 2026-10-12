using UnityEngine;
using UnityEditor;

public class AudioSetup
{
    [MenuItem("Doctocliq/Setup Audio - Asignar clips")]
    public static void SetupAudio()
    {
        Debug.Log("Asignando audio clips...");

        try
        {
            AssignAudioManagerClips();
            AssignItemPickupClips();
            AssignGuardianAIClips();
            AssignPlayerHealthClips();
            AssignObjectiveManagerClips();
            AssignGameManagerClips();

            Debug.Log("Audio setup completado!");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error: {ex.Message}");
        }
    }

    private static void AssignAudioManagerClips()
    {
        AudioManager audioManager = GameObject.FindObjectOfType<AudioManager>();
        if (audioManager == null) return;

        AudioClip backgroundMusic = LoadAudioClip("1 - fondo.mp3");
        if (backgroundMusic != null)
        {
            SerializedObject so = new SerializedObject(audioManager);
            so.FindProperty("backgroundMusic").objectReferenceValue = backgroundMusic;
            so.ApplyModifiedProperties();
            Debug.Log("AudioManager: Música de fondo asignada");
        }
    }

    private static void AssignItemPickupClips()
    {
        ItemPickup[] items = GameObject.FindObjectsOfType<ItemPickup>();
        AudioClip pickupSound = LoadAudioClip("2 - item_pickup.mp3");

        foreach (ItemPickup item in items)
        {
            if (pickupSound != null)
            {
                SerializedObject so = new SerializedObject(item);
                so.FindProperty("pickupSound").objectReferenceValue = pickupSound;
                so.ApplyModifiedProperties();
            }
        }

        if (pickupSound != null)
            Debug.Log($"ItemPickup: Sonido de recolección asignado a {items.Length} items");
    }

    private static void AssignGuardianAIClips()
    {
        GuardianAI guardian = GameObject.FindObjectOfType<GuardianAI>();
        if (guardian == null) return;

        AudioClip alertSound = LoadAudioClip("4 - guardian_alert.mp3");
        AudioClip attackSound = LoadAudioClip("5 - hit_impact.mp3");

        SerializedObject so = new SerializedObject(guardian);

        if (alertSound != null)
            so.FindProperty("alertSound").objectReferenceValue = alertSound;

        if (attackSound != null)
            so.FindProperty("attackSound").objectReferenceValue = attackSound;

        so.ApplyModifiedProperties();
        Debug.Log("GuardianAI: Sonidos de alerta y ataque asignados");
    }

    private static void AssignPlayerHealthClips()
    {
        PlayerHealth playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        if (playerHealth == null) return;

        AudioClip damageSound = LoadAudioClip("6 - damage.mp3");

        if (damageSound != null)
        {
            SerializedObject so = new SerializedObject(playerHealth);
            so.FindProperty("damageSound").objectReferenceValue = damageSound;
            so.ApplyModifiedProperties();
            Debug.Log("PlayerHealth: Sonido de daño asignado");
        }
    }

    private static void AssignObjectiveManagerClips()
    {
        ObjectiveManager objectiveManager = GameObject.FindObjectOfType<ObjectiveManager>();
        if (objectiveManager == null) return;

        AudioClip portalSound = LoadAudioClip("8 - victory.mp3");

        if (portalSound != null)
        {
            SerializedObject so = new SerializedObject(objectiveManager);
            so.FindProperty("portalActivationSound").objectReferenceValue = portalSound;
            so.ApplyModifiedProperties();
            Debug.Log("ObjectiveManager: Sonido del portal asignado");
        }
    }

    private static void AssignGameManagerClips()
    {
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager == null) return;

        AudioClip victorySound = LoadAudioClip("8 - victory.mp3");
        AudioClip defeatSound = LoadAudioClip("9 - defeat.mp3");

        SerializedObject so = new SerializedObject(gameManager);

        if (victorySound != null)
            so.FindProperty("victorySound").objectReferenceValue = victorySound;

        if (defeatSound != null)
            so.FindProperty("defeatSound").objectReferenceValue = defeatSound;

        so.ApplyModifiedProperties();
        Debug.Log("GameManager: Sonidos de victoria y derrota asignados");
    }

    private static AudioClip LoadAudioClip(string filename)
    {
        string[] guids = AssetDatabase.FindAssets(filename.Replace(".mp3", ""));

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (path.Contains("Audio") && path.EndsWith(".mp3"))
            {
                AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
                if (clip != null)
                    return clip;
            }
        }

        Debug.LogWarning($"Audio no encontrado: {filename}");
        return null;
    }
}

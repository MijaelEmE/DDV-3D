using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [Header("Dificultad")]
    [SerializeField] private float guardianDetectionRange = 15f;
    [SerializeField] private float guardianChaseSpeed = 5f;
    [SerializeField] private float guardianPatrolSpeed = 2f;

    [Header("Jugador")]
    [SerializeField] private int playerMaxHealth = 3;
    [SerializeField] private float playerKnockbackForce = 5f;

    [Header("Items")]
    [SerializeField] private int itemsToCollect = 3;
    [SerializeField] private float itemFadeOutDuration = 0.5f;

    [Header("Audio")]
    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private float musicVolume = 0.5f;
    [SerializeField] private float sfxVolume = 0.7f;

    private static GameSettings instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameSettings Get() => instance;

    public float GuardianDetectionRange => guardianDetectionRange;
    public float GuardianChaseSpeed => guardianChaseSpeed;
    public float GuardianPatrolSpeed => guardianPatrolSpeed;
    public int PlayerMaxHealth => playerMaxHealth;
    public float PlayerKnockbackForce => playerKnockbackForce;
    public int ItemsToCollect => itemsToCollect;
    public float ItemFadeOutDuration => itemFadeOutDuration;
    public float MasterVolume => masterVolume;
    public float MusicVolume => musicVolume;
    public float SFXVolume => sfxVolume;
}

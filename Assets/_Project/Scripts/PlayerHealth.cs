using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.5f;
    [SerializeField] private AudioClip damageSound;

    private int currentHealth;
    private Rigidbody rb;
    private GameManager gameManager;
    private float knockbackTimer;

    public System.Action<int> onHealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (knockbackTimer > 0) return;

        GuardianAI guardian = other.GetComponent<GuardianAI>();
        if (guardian != null && guardian.IsAttacking())
        {
            TakeDamage(1, other.transform.position);
        }
    }

    public void TakeDamage(int damage, Vector3 sourcePosition)
    {
        currentHealth -= damage;
        onHealthChanged?.Invoke(currentHealth);

        if (damageSound != null)
            AudioSource.PlayClipAtPoint(damageSound, transform.position);

        ApplyKnockback(sourcePosition);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void ApplyKnockback(Vector3 sourcePosition)
    {
        if (rb != null)
        {
            Vector3 direction = (transform.position - sourcePosition).normalized;
            rb.velocity = direction * knockbackForce;
            knockbackTimer = knockbackDuration;
        }
    }

    private void Die()
    {
        if (gameManager != null)
            gameManager.Defeat();
        else
            Debug.LogError("GameManager no encontrado");
    }

    public int GetHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;

    private void Update()
    {
        if (knockbackTimer > 0)
            knockbackTimer -= Time.deltaTime;
    }
}

using UnityEngine;

public class PortalExit : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private GameManager gameManager;
    private bool triggered = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag(playerTag)) return;

        triggered = true;

        if (gameManager != null)
            gameManager.Victory();
    }
}

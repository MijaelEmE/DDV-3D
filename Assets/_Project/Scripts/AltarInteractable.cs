using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class AltarInteractable : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private bool activateOnce = true;
    [SerializeField] private float activationDelay = 0.1f;
    [SerializeField] private AudioClip activationSound;

    public UnityEvent onActivated;

    private bool activated;
    private float lastActivationTime;

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        if (activated && activateOnce) return;

        float timeSinceLastActivation = Time.time - lastActivationTime;
        if (timeSinceLastActivation < activationDelay) return;

        activated = true;
        lastActivationTime = Time.time;

        if (activationSound != null)
            AudioSource.PlayClipAtPoint(activationSound, transform.position);

        onActivated.Invoke();
    }
}

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class AltarInteractable : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private bool activateOnce = true;

    public UnityEvent onActivated;

    private bool activated;

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated && activateOnce) return;
        if (!other.CompareTag(playerTag)) return;

        activated = true;
        onActivated.Invoke();
    }
}

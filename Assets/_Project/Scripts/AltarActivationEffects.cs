using System.Collections;
using UnityEngine;

public class AltarActivationEffects : MonoBehaviour
{
    [SerializeField] private AltarInteractable altar;
    [SerializeField] private Light altarLight;
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private float messageDuration = 4f;

    private void Awake()
    {
        altar.onActivated.AddListener(Activate);
    }

    private void Activate()
    {
        if (altarLight != null) altarLight.enabled = true;
        if (gate != null) gate.SetActive(false);
        if (messagePanel != null) StartCoroutine(ShowMessage());
    }

    private IEnumerator ShowMessage()
    {
        messagePanel.SetActive(true);
        yield return new WaitForSeconds(messageDuration);
        messagePanel.SetActive(false);
    }
}

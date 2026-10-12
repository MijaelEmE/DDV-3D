using System.Collections;
using UnityEngine;

public class AltarActivationEffects : MonoBehaviour
{
    [SerializeField] private AltarInteractable altar;
    [SerializeField] private Light altarLight;
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private ParticleSystem activationParticles;
    [SerializeField] private float messageDuration = 4f;
    [SerializeField] private AudioClip activationEffectSound;

    private void Awake()
    {
        if (altar != null)
            altar.onActivated.AddListener(Activate);
    }

    private void Activate()
    {
        ActivateLight();
        DeactivateGate();
        PlayParticles();
        PlaySound();
        ShowMessage();
    }

    private void ActivateLight()
    {
        if (altarLight != null)
        {
            altarLight.enabled = true;
            StartCoroutine(PulseLight());
        }
    }

    private IEnumerator PulseLight()
    {
        float elapsed = 0f;
        float pulseDuration = 1f;
        float originalIntensity = altarLight.intensity;

        while (elapsed < pulseDuration)
        {
            elapsed += Time.deltaTime;
            float pulse = Mathf.Sin(elapsed * Mathf.PI) * 0.5f + 1f;
            altarLight.intensity = originalIntensity * pulse;
            yield return null;
        }

        altarLight.intensity = originalIntensity;
    }

    private void DeactivateGate()
    {
        if (gate != null)
            gate.SetActive(false);
    }

    private void PlayParticles()
    {
        if (activationParticles != null)
            activationParticles.Play();
    }

    private void PlaySound()
    {
        if (activationEffectSound != null)
            AudioSource.PlayClipAtPoint(activationEffectSound, transform.position);
    }

    private void ShowMessage()
    {
        if (messagePanel != null)
            StartCoroutine(ShowMessageCoroutine());
    }

    private IEnumerator ShowMessageCoroutine()
    {
        messagePanel.SetActive(true);
        yield return new WaitForSeconds(messageDuration);
        messagePanel.SetActive(false);
    }
}

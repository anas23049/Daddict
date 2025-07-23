using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using Characters;

public class BoosterEffect : MonoBehaviour
{
    public float boostMultiplier = 2f;
    public float boostDuration = 100f;
    public Slider boostSlider;
    public Volume boostVolume;

    private Bloom bloom;
    private ColorAdjustments colorAdjustments;
    private Vignette vignette;

    private bool isBoosting = false;
    private Collider playerCollider;

    [Header("Booster Effect Particle")]
    public ParticleSystem boosterParticle;

    private void Start()
    {
        playerCollider = GetComponent<Collider>();

        if (boostVolume != null && boostVolume.profile != null)
        {
            boostVolume.profile.TryGet(out bloom);
            boostVolume.profile.TryGet(out colorAdjustments);
            boostVolume.profile.TryGet(out vignette);
        }

        if (boostSlider != null)
        {
            boostSlider.gameObject.SetActive(false);
            boostSlider.value = 0f;
        }

        // Ensure particle system is stopped initially
        if (boosterParticle != null)
            boosterParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("boaster") && !isBoosting)
        {
            Destroy(other.gameObject); // Destroy the booster

            Stats stats = GetComponent<Stats>();
            if (stats != null)
            {
                StartCoroutine(ApplyBoost(stats));
            }
        }
    }

    private IEnumerator ApplyBoost(Stats stats)
    {
        isBoosting = true;

        // Show UI
        if (boostSlider != null)
        {
            boostSlider.gameObject.SetActive(true);
            boostSlider.value = 100f;
        }

        // Play particle system
        if (boosterParticle != null && !boosterParticle.isPlaying)
            boosterParticle.Play();

        // Save original stats
        float originalSpeed = stats.speed;
        float originalAcceleration = stats.acceleration;
        float originalAirSpeed = stats.airSpeed;

        // Apply boost
        stats.speed *= boostMultiplier;
        stats.acceleration *= boostMultiplier;
        stats.airSpeed += 10;

        float elapsed = 0f;
        float checkInterval = 0.2f;

        while (elapsed < boostDuration)
        {
            elapsed += Time.deltaTime;

            if (boostSlider != null)
                boostSlider.value = Mathf.Lerp(100f, 0f, elapsed / boostDuration);

            if (elapsed % checkInterval < Time.deltaTime)
            {
                Collider[] allColliders = FindObjectsOfType<Collider>();
                foreach (Collider col in allColliders)
                {
                    if (col != null && col.gameObject != null && col != playerCollider)
                    {
                        if (col.CompareTag("drug") || col.CompareTag("logenemy"))
                        {
                            Physics.IgnoreCollision(playerCollider, col, true);
                        }
                    }
                }
            }

            yield return null;
        }

        // Restore stats
        stats.speed = originalSpeed;
        stats.acceleration = originalAcceleration;
        stats.airSpeed = originalAirSpeed;

        // Re-enable collisions
        Collider[] allToReset = FindObjectsOfType<Collider>();
        foreach (Collider col in allToReset)
        {
            if (col != null && col.gameObject != null && col != playerCollider)
            {
                if (col.CompareTag("drug") || col.CompareTag("logenemy"))
                {
                    Physics.IgnoreCollision(playerCollider, col, false);
                }
            }
        }

        // Stop particle system
        if (boosterParticle != null && boosterParticle.isPlaying)
            boosterParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        // Disable post-processing effects
        if (bloom != null) bloom.intensity.Override(0f);
        if (colorAdjustments != null) colorAdjustments.saturation.Override(0f);
        if (vignette != null) vignette.intensity.Override(0f);

        // Hide slider
        if (boostSlider != null)
        {
            boostSlider.value = 0f;
            boostSlider.gameObject.SetActive(false);
        }

        isBoosting = false;
    }
}

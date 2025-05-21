using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using Characters;

public class BoosterEffect : MonoBehaviour
{
    public float boostMultiplier = 2f;
    public float boostDuration = 5f;
    public Slider boostSlider;
    public Volume boostVolume; // Must be assigned in Inspector

    private Bloom bloom;
    private ColorAdjustments colorAdjustments;
    private Vignette vignette;

    private bool isBoosting = false;

    private void Start()
    {
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

        // Save original stats
        float originalSpeed = stats.speed;
        float originalAcceleration = stats.acceleration;
        float orignaiairspeed = stats.airSpeed;

        // Apply boost
        stats.speed *= boostMultiplier;
        stats.acceleration *= boostMultiplier;
        stats.airSpeed += 10;

        // Enable visual effects
        if (bloom != null) bloom.intensity.Override(1f);
        if (colorAdjustments != null) colorAdjustments.saturation.Override(30f);
        if (vignette != null) vignette.intensity.Override(0.4f);

        float elapsed = 0f;
        while (elapsed < boostDuration)
        {
            elapsed += Time.deltaTime;
            if (boostSlider != null)
                boostSlider.value = Mathf.Lerp(100f, 0f, elapsed / boostDuration);
            yield return null;
        }

        // Restore stats
        stats.airSpeed = orignaiairspeed;
        stats.acceleration = originalAcceleration;
        stats.speed= originalSpeed;
        // Disable effects
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

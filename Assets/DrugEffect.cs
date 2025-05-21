using Characters;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DrugEffect : MonoBehaviour
{
    public Slider drugSlider;
    public Image drugEffectOverlay;

    [Header("Post Processing")]
    public Volume postProcessVolume;

    private Vignette vignette;
    private ColorAdjustments colorAdjustments;
    private Bloom bloom;
    private LensDistortion lensDistortion;

    private Stats playerStats;

    private void Start()
    {
        playerStats = GetComponent<Stats>();
        StartCoroutine(ContinuouslyApplyDrugEffect());

        // Get Post Processing Effects from the volume
        if (postProcessVolume != null && postProcessVolume.profile != null)
        {
            postProcessVolume.profile.TryGet(out vignette);
            postProcessVolume.profile.TryGet(out colorAdjustments);
            postProcessVolume.profile.TryGet(out bloom);
            postProcessVolume.profile.TryGet(out lensDistortion);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("drug"))
        {
            StartCoroutine(SmoothChange(+10)); // Smoothly increase by 10
            Destroy(collider.gameObject);
        }
        else if (collider.CompareTag("firstaid"))
        {
            StartCoroutine(SmoothChange(-10)); // Smoothly decrease by 10
            Destroy(collider.gameObject);
        }
    }

    private IEnumerator SmoothChange(float amount)
    {
        float target = Mathf.Clamp(drugSlider.value + amount, 0, 100);
        float speed = 10f; // Change rate per second

        while (!Mathf.Approximately(drugSlider.value, target))
        {
            drugSlider.value = Mathf.MoveTowards(drugSlider.value, target, speed * Time.deltaTime);
            yield return null;
        }

        drugSlider.value = target; // Ensure it's exact
    }

    private IEnumerator ContinuouslyApplyDrugEffect()
    {
        while (true)
        {
            float drugValue = Mathf.Clamp(drugSlider.value, 0, 100);
            float t = drugValue / 100f;

            // Stat effects
            float minMultiplier = 0.8f;
        

            // UI overlay pulse effect
            if (drugEffectOverlay != null)
            {
                float pulse = Mathf.Sin(Time.time * 2f) * 0.5f + 0.5f;
                float alpha = Mathf.Lerp(0f, 0.6f, t) * pulse;
                Color c = drugEffectOverlay.color;
                c.a = alpha;
                drugEffectOverlay.color = c;
            }

            // Vignette effect
            if (vignette != null)
            {
                vignette.intensity.value = Mathf.Lerp(0f, 0.5f, t);
                vignette.smoothness.value = 0.9f;
            }

            // Color desaturation
            if (colorAdjustments != null)
            {
                colorAdjustments.saturation.value = Mathf.Lerp(0f, -80f, t);
                colorAdjustments.contrast.value = Mathf.Lerp(0f, 30f, t);
                colorAdjustments.postExposure.value = Mathf.Lerp(0f, 0.5f, t);
            }

            // Bloom (glow effect)
            if (bloom != null)
            {
                bloom.intensity.value = Mathf.Lerp(0f, 2f, t);
                bloom.threshold.value = Mathf.Lerp(1f, 0.8f, t);
            }

            // Lens Distortion (trippy warping)
            if (lensDistortion != null)
            {
                lensDistortion.intensity.value = Mathf.Lerp(0f, -0.6f, t);
                lensDistortion.scale.value = Mathf.Lerp(1f, 0.9f, t);
            }

            yield return null;
        }
    }
}

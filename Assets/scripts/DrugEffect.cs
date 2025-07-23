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

    [Header("Audio")]
    public AudioSource audioSource;         // Used for drug pickup/first aid sounds and background music
    public AudioSource drugLoopSource;      // NEW: Used only for looping drug effect sound
    public AudioClip drugSound;
    public AudioClip Severedrugsound;
    public AudioClip firstAidSound;

    private Vignette vignette;
    private ColorAdjustments colorAdjustments;
    private Bloom bloom;
    private LensDistortion lensDistortion;

    private Stats playerStats;
    private Coroutine smoothChangeCoroutine;

    [Header("Drug Particle Effect")]
    
    private ParticleSystem.MainModule particleMain;
    private bool particlesEnabled = false;

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

        // Auto-assign AudioSource if not set
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Set up drug loop audio source
        if (drugLoopSource == null)
        {
            drugLoopSource = gameObject.AddComponent<AudioSource>();
            drugLoopSource.loop = true;
            drugLoopSource.playOnAwake = false;
        }

        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("drug"))
        {
            if (drugSound != null && audioSource != null)
                audioSource.PlayOneShot(drugSound);

            if (smoothChangeCoroutine != null)
                StopCoroutine(smoothChangeCoroutine);
            smoothChangeCoroutine = StartCoroutine(SmoothChange(+10));

            Destroy(collider.gameObject);
        }
        else if (collider.CompareTag("firstaid"))
        {
            if (firstAidSound != null && audioSource != null)
                audioSource.PlayOneShot(firstAidSound);

            if (smoothChangeCoroutine != null)
                StopCoroutine(smoothChangeCoroutine);
            smoothChangeCoroutine = StartCoroutine(SmoothChange(-10));

            Destroy(collider.gameObject);
        }
    }

    private IEnumerator SmoothChange(float amount)
    {
        float target = Mathf.Clamp(drugSlider.value + amount, 0, 100);
        float speed = 10f;

        while (!Mathf.Approximately(drugSlider.value, target))
        {
            drugSlider.value = Mathf.MoveTowards(drugSlider.value, target, speed * Time.deltaTime);
            yield return null;
        }

        drugSlider.value = target;
    }

    private IEnumerator ContinuouslyApplyDrugEffect()
    {
        bool isLoopingSoundPlaying = false;

        while (true)
        {
            float drugValue = Mathf.Clamp(drugSlider.value, 0, 100);
            float t = drugValue / 100f;

            // UI overlay pulse effect
            if (drugEffectOverlay != null)
            {
                float pulse = Mathf.Sin(Time.time * 2f) * 0.5f + 0.5f;
                float alpha = Mathf.Lerp(0f, 0.6f, t) * pulse;
                Color c = drugEffectOverlay.color;
                c.a = alpha;
                drugEffectOverlay.color = c;
            }

            // Vignette
            if (vignette != null)
            {
                vignette.intensity.value = Mathf.Lerp(0f, 0.5f, t);
                vignette.smoothness.value = 0.9f;
            }

            // Color Adjustments
            if (colorAdjustments != null)
            {
                colorAdjustments.saturation.value = Mathf.Lerp(0f, -80f, t);
                colorAdjustments.contrast.value = Mathf.Lerp(0f, 30f, t);
                colorAdjustments.postExposure.value = Mathf.Lerp(0f, 0.5f, t);
            }

            // Bloom
            if (bloom != null)
            {
                bloom.intensity.value = Mathf.Lerp(0f, 2f, t);
                bloom.threshold.value = Mathf.Lerp(1f, 0.8f, t);
            }

            // Lens Distortion
            if (lensDistortion != null)
            {
                lensDistortion.intensity.value = Mathf.Lerp(0f, -0.6f, t);
                lensDistortion.scale.value = Mathf.Lerp(1f, 0.9f, t);
            }

            // Looping severe drug sound
            if (drugValue >= 60f)
            {
                if (!isLoopingSoundPlaying && drugLoopSource != null && Severedrugsound != null)
                {
                    drugLoopSource.clip = Severedrugsound;
                    drugLoopSource.Play();
                    isLoopingSoundPlaying = true;
                }
            }
            else
            {
                if (isLoopingSoundPlaying && drugLoopSource != null)
                {
                    drugLoopSource.Stop();
                    drugLoopSource.clip = null;
                    isLoopingSoundPlaying = false;
                }
            }

            //// Particle fade logic
            //if (drugParticleSystem != null)
            //{
            //    if (drugValue > 50f)
            //    {
            //        if (!particlesEnabled)
            //        {
            //            drugParticleSystem.Play();
            //            particlesEnabled = true;
            //        }

            //        float fadeT = Mathf.InverseLerp(50f, 100f, drugValue);
            //        Color startColor = particleMain.startColor.color;
            //        startColor.a = Mathf.Lerp(0f, 1f, fadeT);
            //        particleMain.startColor = new ParticleSystem.MinMaxGradient(startColor);
            //    }
            //    else
            //    {
            //        if (particlesEnabled)
            //        {
            //            drugParticleSystem.Stop();
            //            particlesEnabled = false;
            //        }
            //    }
            //}

            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using Animators;

public class lifesystem : MonoBehaviour
{
    public Image[] lifeIcons;
    public GameObject virtualCamera;
    public GameObject mainCamera;
    public MonoBehaviour playerMovement;
    public Animator playerAnimator;

    private int currentLives = 5;
    private bool isDead = false;
    private CharacterAnimator characterAnimator;
    public Canvas gameplayCanvas1;
    public Canvas gameplayCanvas2;
    public Canvas gameOverCanvas;
    public AudioSource backgroundMusic;
    public AudioClip deathSound;

    public AudioSource audioSource;
    public AudioClip enemyhitsound;
    public AudioSource audioSOURCE;
    public AudioClip lifecollect;
    public GameObject deathParticlePrefab;

    private void Start()
    {
        UpdateLifeUI();
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("logenemy"))
        {
            if (enemyhitsound != null && audioSource != null)
                audioSource.PlayOneShot(enemyhitsound);

            if (currentLives > 0)
            {
                currentLives--;
                UpdateLifeUI();

                if (currentLives == 0)
                {
                    StartCoroutine(HandleDeath());
                }
            }

            Destroy(other.gameObject);
        }

        if (other.CompareTag("life"))
        {
            if (lifecollect != null && audioSOURCE != null)
                audioSOURCE.PlayOneShot(lifecollect);

            if (currentLives < lifeIcons.Length)
            {
                currentLives++;
                UpdateLifeUI();
            }

            Destroy(other.gameObject);
        }
    }

    private void UpdateLifeUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = i < currentLives;
        }
    }

    private IEnumerator HandleDeath()
    {
        isDead = true;

        playerAnimator.Play("Dead", 0, 0f);
        playerAnimator.Update(0f);

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            while (rb.linearVelocity.magnitude > 0.1f)
            {
                rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.deltaTime * 2f);
                yield return null;
            }
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        if (virtualCamera != null) virtualCamera.SetActive(false);
        if (mainCamera != null) mainCamera.SetActive(true);

        if (playerAnimator != null)
        {
            playerAnimator.SetFloat("inputMagnitude", 0f);
            playerAnimator.SetFloat("verInput", 0f);
            playerAnimator.SetFloat("horInput", 0f);
            playerAnimator.SetFloat("groundVelocity", 0f);
            playerAnimator.SetBool("isFalling", false);
            playerAnimator.SetBool("isAboutToLand", false);
            playerAnimator.SetBool("crouching", false);
            playerAnimator.SetBool("unskippable", false);
            playerAnimator.SetBool("sliding", false);
            playerAnimator.SetBool("attacking", false);

            playerAnimator.ResetTrigger("comboAttack");
            playerAnimator.ResetTrigger("fastAttack");
            playerAnimator.ResetTrigger("strongAttack");

            for (int i = 1; i < playerAnimator.layerCount; i++)
            {
                playerAnimator.SetLayerWeight(i, 0f);
            }

            if (deathParticlePrefab != null)
            {
                Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
            }

            yield return null;
        }

        if (characterAnimator != null)
        {
            characterAnimator.enabled = false;
        }

        playerAnimator.Play("Dead", 0, 0f);
        playerAnimator.Update(0f);

        yield return new WaitForSeconds(0.5f);

        // ✅ Stop background music and play death sound separately
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        yield return new WaitForSeconds(3f);

        if (gameplayCanvas1 != null) gameplayCanvas1.gameObject.SetActive(false);
        if (gameplayCanvas2 != null) gameplayCanvas2.gameObject.SetActive(false);
        if (gameOverCanvas != null) gameOverCanvas.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

    public bool IsDead => isDead;
}

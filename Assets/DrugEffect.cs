using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrugEffect : MonoBehaviour
{
    public Slider drugSlider;
    private Stats playerStats;

    private void Start()
    {
        playerStats = GetComponent<Stats>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("drug"))
        {
            drugSlider.value += 10;
            Destroy(collider.gameObject);
            ApplyDrugEffects();
        }
    }
    public void ApplyDrugEffects()
    {
        StartCoroutine(ApplyTemporaryDrugEffects());
    }

    private IEnumerator ApplyTemporaryDrugEffects()
    {
        float originalSpeed = playerStats.speed;
        float originalAcceleration = playerStats.acceleration;
        float originalJumpForce = playerStats.jumpForce;
        float originalHealth = playerStats.health;
        
        playerStats.speed *= 0.8f;
        playerStats.acceleration *= 0.8f;
        playerStats.jumpForce *= 0.8f;
        playerStats.health -= 10f;
     
        yield return new WaitForSeconds(2f);
   
        playerStats.speed = originalSpeed;
        playerStats.acceleration = originalAcceleration;
        playerStats.jumpForce = originalJumpForce;
        playerStats.health = originalHealth;
        //playerStats.additionalGravityForce = originalGravity;
    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrugEffect : MonoBehaviour
{
    public Slider drugSlider;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("drug"))
        {
            drugSlider.value += 10;
            Destroy(collider.gameObject);
        }
    }
}

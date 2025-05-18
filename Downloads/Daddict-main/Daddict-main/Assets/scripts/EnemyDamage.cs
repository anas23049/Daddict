using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 20;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }

            Destroy(gameObject); // Destroy enemy on contact
        }
    }
}

using UnityEngine;

namespace Characters
{
    public class Stats : MonoBehaviour
    {
        [Header("Movement speed")]
        public float speed = 10f;
        public float acceleration = 30f;
        public float airSpeed = 5f;
        public float rotationSpeed = 0.1f;
        public float crouchSpeed = 6f;
        public float slidingSpeed = 15f;

        [Header("Jumping/falling")]
        public float jumpForce = 10f;
        public float additionalGravityForce = 20f;
        public int maxJumps = 2;
        public float maxDownVelocity = -20f;

        [Header("Health")]
        public float health = 100f;
        public float invincibilityTime = 2f;

        // Add base values to restore original stats later
        [HideInInspector] public float baseSpeed;
        [HideInInspector] public float baseAcceleration;
        [HideInInspector] public float baseJumpForce;
        [HideInInspector] public float baseHealth;

        private void Awake()
        {
            baseSpeed = speed;
            baseAcceleration = acceleration;
            baseJumpForce = jumpForce;
            baseHealth = health;
        }
    }
}

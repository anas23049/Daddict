using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Animator))]
    public class ThirdPersonCharacter : MonoBehaviour
    {
        [SerializeField] float m_JumpPower = 12f;
        [SerializeField] float m_GravityMultiplier = 2f;
        [SerializeField] float m_MoveSpeedMultiplier = 1f;
        [SerializeField] float m_AnimSpeedMultiplier = 1f;
        [SerializeField] float m_GroundCheckDistance = 0.1f;
        [SerializeField] private GameObject m_DustPrefab;
        [SerializeField] private Transform m_DustSpawnPoint;

        private float m_DustTimer = 0f;
        private GameObject m_LastDustInstance;




        private Rigidbody m_Rigidbody;
        private Animator m_Animator;
        private CapsuleCollider m_Capsule;
       

        private float m_ForwardAmount;
        private float m_OrigGroundCheckDistance;
        private float m_CapsuleHeight;
        private Vector3 m_CapsuleCenter;
        private Vector3 m_GroundNormal;
        private bool m_IsGrounded;

        private bool m_Sliding = false;
        private float m_SlideDuration = 1f;
        private float m_SlideTimer = 0f;

        void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            m_Animator.applyRootMotion = false;

            m_CapsuleHeight = m_Capsule.height;
            m_CapsuleCenter = m_Capsule.center;

            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            m_OrigGroundCheckDistance = m_GroundCheckDistance;
        }

        public void Move(Vector3 move, bool slideInput, bool jump)
        {
            if (move.magnitude > 1f) move.Normalize();

            move = new Vector3(move.x, 0, 0); // 2.5D: Only allow X axis movement

            CheckGroundStatus();
            if (m_IsGrounded && Mathf.Abs(move.x) > 0.01f) // only if moving on ground
            {
                m_DustTimer += Time.deltaTime;
                if (m_DustTimer >= 0.5f)
                {
                    if (m_LastDustInstance != null)
                    {
                        Destroy(m_LastDustInstance);
                    }

                    m_LastDustInstance = Instantiate(m_DustPrefab, m_DustSpawnPoint.position, Quaternion.identity);
                    Destroy(m_LastDustInstance, 3f); // Auto-destroy in 1 second
                    m_DustTimer = 0f;
                }
            }
            else
            {
                m_DustTimer = 0f; // reset if not grounded or not moving
            }

            move = Vector3.ProjectOnPlane(move, m_GroundNormal);
            m_ForwardAmount = move.x;

            if (m_IsGrounded)
            {
                HandleGroundedMovement(slideInput, jump);

                // Trigger slide ONLY if grounded and swipe input received, and not already sliding
                if (slideInput && !m_Sliding)
                {
                    TriggerSlide();
                }
            }
            else
            {
                HandleAirborneMovement();
            }
           

           
            // Update slide timer
            if (m_Sliding)
            {
                m_SlideTimer -= Time.deltaTime;
                if (m_SlideTimer <= 0f)
                {
                    EndSlide();
                }
            }

            // Apply horizontal velocity (preserve Y velocity for gravity/jumping)
            Vector3 targetVelocity = new Vector3(move.x * m_MoveSpeedMultiplier, m_Rigidbody.velocity.y, 0);
            m_Rigidbody.velocity = targetVelocity;

            UpdateAnimator(move);
        }

        public void TriggerSlide()
        {
            if (m_IsGrounded && !m_Sliding)
            {
                m_Sliding = true;
                m_SlideTimer = m_SlideDuration;

                m_Animator.SetTrigger("Slide");
                m_Animator.SetBool("Sliding", true);

                m_Capsule.height = m_CapsuleHeight / 2f;
                m_Capsule.center = m_CapsuleCenter / 2f;

                // Boost slide forward velocity
                float slideSpeed = m_MoveSpeedMultiplier * 2f; // Increase this multiplier if needed
                m_Rigidbody.velocity = new Vector3(Mathf.Sign(m_ForwardAmount) * slideSpeed, m_Rigidbody.velocity.y, 0);
            }
        }


        void EndSlide()
        {
            m_Sliding = false;
            m_Animator.SetBool("Sliding", false);

            m_Capsule.height = m_CapsuleHeight;
            m_Capsule.center = m_CapsuleCenter;
        }

        void HandleGroundedMovement(bool slideInput, bool jump)
        {
            if (jump && !slideInput && !m_Sliding && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
            {
                m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, 0);
                m_IsGrounded = false;
                m_GroundCheckDistance = 0.1f;
            }
        }

        void HandleAirborneMovement()
        {
            Vector3 extraGravity = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
            m_Rigidbody.AddForce(extraGravity);
            m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
        }

        void UpdateAnimator(Vector3 move)
        {
            m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
            m_Animator.SetBool("OnGround", m_IsGrounded);
            m_Animator.SetBool("Sliding", m_Sliding);

            if (!m_IsGrounded)
                m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);

            m_Animator.speed = (m_IsGrounded && move.magnitude > 0) ? m_AnimSpeedMultiplier : 1;
        }

        void CheckGroundStatus()
        {
            RaycastHit hit;
#if UNITY_EDITOR
            Debug.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * 0.1f + Vector3.down * m_GroundCheckDistance, Color.yellow);
#endif
            if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, m_GroundCheckDistance))
            {
                m_GroundNormal = hit.normal;
                m_IsGrounded = true;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundNormal = Vector3.up;
            }
        }
    }
}

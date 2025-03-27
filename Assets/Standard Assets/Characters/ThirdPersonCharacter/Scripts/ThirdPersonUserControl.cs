using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character;
        private Transform m_Cam;
        private Vector3 m_CamForward;
        private Vector3 m_Move;
        private bool m_Jump;
        private bool m_Crouch;

        public float autoMoveSpeed = 2.5f; // Speed for automatic forward movement

        private Vector2 touchStartPos;
        private bool isSwiping = false;

        private void Start()
        {
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning("Warning: No main camera found. Assign a camera tagged \"MainCamera\".", gameObject);
            }

            m_Character = GetComponent<ThirdPersonCharacter>();
        }

        private void Update()
        {
            DetectSwipe(); // Call swipe detection function
        }

        private void FixedUpdate()
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");

            // Automatic forward movement (Right direction in X-axis)
            m_Move = new Vector3(autoMoveSpeed, 0, h);

            // Pass movement parameters to the character controller
            m_Character.Move(m_Move, m_Crouch, m_Jump);

            // Reset jump after execution
            m_Jump = false;
        }

        private void DetectSwipe()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    touchStartPos = touch.position;
                    isSwiping = true;
                }
                else if (touch.phase == TouchPhase.Ended && isSwiping)
                {
                    Vector2 touchEndPos = touch.position;
                    Vector2 swipeDelta = touchEndPos - touchStartPos;

                    if (Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x)) // Vertical Swipe
                    {
                        if (swipeDelta.y > 50) // Swipe Up → Jump
                        {
                            m_Jump = true;
                        }
                        else if (swipeDelta.y < -50) // Swipe Down → Crouch
                        {
                            m_Crouch = true;
                        }
                    }
                    isSwiping = false;
                }

                if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
                {
                    m_Crouch = false; // Stop crouching when touch ends
                }
            }
        }
    }
}


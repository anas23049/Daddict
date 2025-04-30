using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character;
        private Vector3 m_Move;
        private bool m_Jump;
        public float autoMoveSpeed = 2.5f;
        private Vector2 touchStartPos;
        private bool isSwiping = false;

        private void Start()
        {
            m_Character = GetComponent<ThirdPersonCharacter>();
        }

        private void Update()
        {
            DetectSwipe();
        }

        private void FixedUpdate()
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");

            m_Move = new Vector3(autoMoveSpeed, 0, h);
            m_Character.Move(m_Move, false, m_Jump); // We use TriggerSlide() separately

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
                    Vector2 swipeDelta = touch.position - touchStartPos;

                    if (Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x))
                    {
                        if (swipeDelta.y > 50)
                        {
                            m_Jump = true;
                        }
                        else if (swipeDelta.y < -50)
                        {
                            m_Character.TriggerSlide();
                        }
                    }

                    isSwiping = false;
                }
            }
        }
    }
}

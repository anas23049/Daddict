using Characters.Movement;
using Characters.Movement.Behaviours;
using Player;
using UnityEngine;

namespace General
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] public GameObject playerGameObject;
        private static IPlayer _player;
        private lifesystem _lifeSystem;

        private Vector2 touchStartPos;
        private Vector2 touchEndPos;
        private bool isSwiping = false;
        private float minSwipeDistance = 50f; // Minimum distance for swipe to register

        private void Start()
        {
            _player = playerGameObject.GetComponent<IPlayer>();
            _lifeSystem = playerGameObject.GetComponent<lifesystem>();
        }

        private void Update()
        {
            if (_lifeSystem != null && _lifeSystem.IsDead) return;

            HandleKeyboardInput();
            HandleTouchInput();
        }

        private void HandleKeyboardInput()
        {
            if (Input.GetButtonDown("Jump"))
            {
                (_player.getMovement() as IJumpable)?.Jump();
            }

            if (Input.GetButtonDown("Slide"))
            {
                _player.ChangeMovement(MovementEnum.Slide);
            }

            if (Input.GetButtonUp("Slide"))
            {
                _player.ChangeMovement(MovementEnum.Ground);
            }
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        isSwiping = true;
                        touchStartPos = touch.position;
                        break;

                    case TouchPhase.Moved:
                        touchEndPos = touch.position;
                        break;

                    case TouchPhase.Ended:
                        if (!isSwiping) return;

                        float swipeDistance = (touchEndPos - touchStartPos).magnitude;
                        if (swipeDistance > minSwipeDistance)
                        {
                            Vector2 swipeDirection = touchEndPos - touchStartPos;
                            if (Mathf.Abs(swipeDirection.y) > Mathf.Abs(swipeDirection.x))
                            {
                                if (swipeDirection.y > 0)
                                {
                                    // Swipe up
                                    (_player.getMovement() as IJumpable)?.Jump();
                                }
                                else
                                {
                                    // Swipe down
                                    _player.ChangeMovement(MovementEnum.Slide);
                                    // Optional: auto-return to ground movement after short delay
                                    Invoke(nameof(ResetToGround), 0.5f);
                                }
                            }
                        }

                        isSwiping = false;
                        break;
                }
            }
        }

        private void ResetToGround()
        {
            _player.ChangeMovement(MovementEnum.Ground);
        }

        public static float getHorInput() => 10f;
        public static float getVerInput() => 0f;
        public static float getMagnitude() => 1f;
    }
}

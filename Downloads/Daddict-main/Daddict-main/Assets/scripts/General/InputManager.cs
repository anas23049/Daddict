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

        private void Start()
        {
            _player = playerGameObject.GetComponent<IPlayer>();
        }

        private void Update()
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

        // These are no longer needed, but left here if used elsewhere
        public static float getHorInput() => 10f; // Always moving right
        public static float getVerInput() => 0f;
        public static float getMagnitude() => 1f;
    }
}

using System.Collections.Generic;
using Animators;
using Characters;
using Characters.Movement;
using General.Util;
using Player.MovementDirection;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Stats))]
    public class Player : MonoBehaviour, IPlayer
    {
        private static readonly FastEnumIntEqualityComparer<MovementEnum> FastEnumIntEqualityComparer =
            new FastEnumIntEqualityComparer<MovementEnum>();

        private readonly Dictionary<MovementEnum, IMovement> _movements =
            new Dictionary<MovementEnum, IMovement>(FastEnumIntEqualityComparer);

        private IMovement _movement;
        private IAnimatorFacade _animatorFacade;
        private Rigidbody _rbd;
        private Stats _stats;

        private void Start()
        {
            _rbd = GetComponent<Rigidbody>();
            _stats = GetComponent<Stats>();
            _animatorFacade = new AnimatorFacade(GetComponentInChildren<ICharacterAnimator>(), this);
            InitMovements();
            _movement = _movements[MovementEnum.Ground];
        }

        private void FixedUpdate()
        {
            // Always move in the +X direction (right)
            Vector3 constantXDirection = Vector3.right;
            _movement.Move(constantXDirection);
        }

        public void Die()
        {
            // Implement death logic here if needed
        }

        public IAnimatorFacade getAnimatorFacade() => _animatorFacade;
        public Rigidbody getRigidbody() => _rbd;
        public Transform getTransform() => transform;
        public IMovement getMovement() => _movement;
        public Stats getStats() => _stats;

        public void ChangeMovement(MovementEnum movementEnum)
        {
            _movement.CleanUp();
            _movement = _movements[movementEnum];
            _movement.SetUp();
        }

        private void InitMovements()
        {
            _movements.Add(MovementEnum.Ground, new GroundMovement(this));
            _movements.Add(MovementEnum.Midair, new MidairMovement(this));
            _movements.Add(MovementEnum.Crouch, new CrouchingMovement(this));
            _movements.Add(MovementEnum.Slide, new SlidingMovement(this));
            _movements.Add(MovementEnum.Attack, new AttackingMovement(this));
        }

        public void ChangeMovementDirection(IMovementDirection movementDirection)
        {
            // Not needed for fixed X movement
            throw new System.NotImplementedException();
        }

        public void ChangeMovementDirection(CameraView cameraView)
        {
            // Not needed for fixed X movement
            throw new System.NotImplementedException();
        }
    }
}

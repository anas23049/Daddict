using System;
using Characters.Movement.Behaviours;
using General;
using UnityEngine;

namespace Characters.Movement
{
    public class GroundMovement : AbstractMovement, IJumpable, IFallable
    {
        public bool isBoosting = false; // <-- Added

        public GroundMovement(IPhysicsCharacter character) : base(character)
        {
        }

        public override void SetUp()
        {
        }

        public override void Move(Vector3 direction)
        {
            if (IsFalling())
            {
                ChangeMovement(MovementEnum.Midair);
                return;
            }

            var velocity = AccelerateAndMove(direction);

            Rotate(direction);
            UpdateAnimParameters(velocity);
        }

        private Vector3 AccelerateAndMove(Vector3 direction)
        {
            Vector3 clampedDirection = Vector3.ClampMagnitude(direction, 1f);
            Vector3 velocity = CommonMethods.CreateVectorWithoutLoosingYWithMultiplier(
                clampedDirection, rbd.linearVelocity.y, stats.speed);


            if (isBoosting)
            {
                // Directly override velocity during boost
                rbd.linearVelocity = velocity;
            }
            else if (rbd.linearVelocity.magnitude < velocity.magnitude)
            {
                var acceleration = CommonMethods.CreateVectorWithoutLoosingYWithMultiplier(
                    clampedDirection, rbd.linearVelocity.y, stats.acceleration);
                rbd.AddForce(acceleration);
            }
            else
            {
                rbd.linearVelocity = velocity;
            }

            return velocity;
        }

        private void UpdateAnimParameters(Vector3 groundVelocity)
        {
            animatorFacade.UpdateInputs();
            animatorFacade.SetGroundVelocity(CommonMethods.CalculateGroundVelocity(groundVelocity));
        }

        public void Jump()
        {
            animatorFacade.SetJumping(true);
            rbd.AddForce(Vector3.up * stats.jumpForce, ForceMode.Impulse);
        }

        public override void CleanUp()
        {
            return;
        }

        public bool IsFalling()
        {
            return !CommonMethods.ONGround(transform.position);
        }
    }
}

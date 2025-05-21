using UnityEngine;

namespace Animators
{
    public class CharacterAnimator : MonoBehaviour, ICharacterAnimator
    {
        private Animator _animator;
        private static readonly int InputMagnitude = Animator.StringToHash("inputMagnitude");
        private static readonly int VerInput = Animator.StringToHash("verInput");
        private static readonly int HorInput = Animator.StringToHash("horInput");
        private static readonly int GroundVelocity = Animator.StringToHash("groundVelocity");
        private static readonly int IsFalling = Animator.StringToHash("isFalling");
        private static readonly int IsAboutToLand = Animator.StringToHash("isAboutToLand");
        private static readonly int Crouching = Animator.StringToHash("crouching");
        private static readonly int Unskippable = Animator.StringToHash("unskippable");
        private static readonly int Sliding = Animator.StringToHash("sliding");



        private CapsuleCollider _capsuleCollider;
        private float _originalHeight;
        private Vector3 _originalCenter;

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            _capsuleCollider = GetComponent<CapsuleCollider>();

            if (_capsuleCollider != null)
            {
                _originalHeight = _capsuleCollider.height;
                _originalCenter = _capsuleCollider.center;
            }
        }

        public Animator getAnimator()
        {
            return _animator;
        }

        public void SetHorInput(float horInput)
        {
            _animator.SetFloat(HorInput, horInput);
        }

        public void SetVerInput(float verInput)
        {
            _animator.SetFloat(VerInput, verInput);
        }

        public void SetInputMagnitude(float inputMagnitude)
        {
            _animator.SetFloat(InputMagnitude, inputMagnitude);
        }

        public void SetGroundVelocity(float groundVelocity)
        {
            _animator.SetFloat(GroundVelocity, groundVelocity);
        }

        public void SetIsFalling(bool isFalling)
        {
            _animator.SetBool(IsFalling, isFalling);
        }

        public void SetIsAboutToLand(bool isAboutToLand)
        {
            _animator.SetBool(IsAboutToLand, isAboutToLand);
        }

        public void SetJumping(bool jumping)
        {
            if (jumping)
            {
                _animator.CrossFade("jump", 0.2f);
            }
        }

        public void SetCrouching(bool crouching)
        {
            _animator.SetBool(Crouching, crouching);
        }

        public void SetUnskippable(bool unskippable)
        {
            _animator.SetBool(Unskippable, unskippable);
        }

       

        public void SetSliding(bool sliding)
        {
            _animator.SetBool(Sliding, sliding);

           
                if (sliding)
                {
                    // Reduce height and shift center down
                    _capsuleCollider.height = _originalHeight * 0.5f;
                    _capsuleCollider.center = new Vector3(
                        _originalCenter.x,
                        _originalCenter.y - (_originalHeight * 0.25f),
                        _originalCenter.z
                    );
                }
                else
                {
                    // Restore original height and center
                    _capsuleCollider.height = _originalHeight;
                    _capsuleCollider.center = _originalCenter;
                }
            
        }


        public void TriggerStrongAttack()
        {
            throw new System.NotImplementedException();
        }

        public void TriggerFastAttack()
        {
            throw new System.NotImplementedException();
        }

        public void SetComboAttack()
        {
            throw new System.NotImplementedException();
        }

        public void ResetComboAttack()
        {
            throw new System.NotImplementedException();
        }

        public void ResetStrongAttack()
        {
            throw new System.NotImplementedException();
        }

        public void ResetFastAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}
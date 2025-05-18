using Characters;

using Characters.Movement;
using Characters.Movement.Behaviours;
using UnityEngine;

namespace Animators
{
    public class AnimatorStateFacade : MonoBehaviour, IAnimatorStateFacade
    {

        private Animator _animator;
        private ICharacter _character;

        private void Start()
        {
            _character = GetComponentInParent<ICharacter>();
         
            _animator = GetComponent<Animator>();
        }


        

  

        private void MakeSureCharacterIsNotNull()
        {
            _character = _character ?? GetComponentInParent<ICharacter>();
        }

        private void OnAnimatorMove()
        {
            MakeSureCharacterIsNotNull();
            var movement = _character.getMovement();
            if (!_animator || !(movement is AttackingMovement) || !(movement is IRootMotion)) return;
            ((IRootMotion) movement).SetRootMotionAdditionalPosition(_animator.deltaPosition);
        }

        public void FinishTimeForCombo()
        {
            throw new System.NotImplementedException();
        }
    }
}
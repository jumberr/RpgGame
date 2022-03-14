using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Animation.States
{
    public class IdleAnimationState : IAnimationState
    {
        private Animator _animator;
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");

        public EAnimationState StateName => EAnimationState.Idle;

        public void Construct(Animator animator) => 
            _animator = animator;

        public void Enter() => 
            _animator.SetBool(IsIdle, true);

        public void Exit() => 
            _animator.SetBool(IsIdle, false);
    }
}
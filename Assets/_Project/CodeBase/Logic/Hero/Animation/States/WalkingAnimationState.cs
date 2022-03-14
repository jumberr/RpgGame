using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Animation.States
{
    public class WalkingAnimationState : IAnimationState
    {
        private Animator _animator;
        private static readonly int IsWalk = Animator.StringToHash("IsWalk");

        public EAnimationState StateName => EAnimationState.Walk;

        public void Construct(Animator animator) => 
            _animator = animator;
        
        public void Enter() => 
            _animator.SetBool(IsWalk, true);

        public void Exit() => 
            _animator.SetBool(IsWalk, false);
    }
}
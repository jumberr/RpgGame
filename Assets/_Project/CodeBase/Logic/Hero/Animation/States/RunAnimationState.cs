using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Animation.States
{
    public class RunAnimationState : IAnimationState
    {
        private Animator _animator;
        private static readonly int IsRun = Animator.StringToHash("IsRun");

        public EAnimationState StateName => EAnimationState.Run;

        public void Construct(Animator animator) => 
            _animator = animator;

        public void Enter() => 
            _animator.SetBool(IsRun, true);

        public void Exit() => 
            _animator.SetBool(IsRun, false);
    }
}
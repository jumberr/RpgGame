using _Project.CodeBase.Logic.Hero.Animation.States;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Animation
{
    public class HeroAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private IAnimationStateMachine _stateMachine;

        private void Start()
        {
            _stateMachine = new AnimationStateMachine(_animator);
            EnterIdleState();
        }

        public EAnimationState CurrentState => _stateMachine.GetStateName();
        
        public void EnterIdleState() => 
            _stateMachine.Enter<IdleAnimationState>();

        public void EnterWalkState() =>
            _stateMachine.Enter<WalkingAnimationState>();

        public void EnterRunState() =>
            _stateMachine.Enter<RunAnimationState>();
        
        //public async void PlayTargetAnimation(int animHash, bool applyRootMotion, Action onComplete = null)
        //{
        //    _animator.applyRootMotion = applyRootMotion;
        //    _animator.CrossFade(animHash, 0.2f);
        //    await OnEndAnimation(onComplete);
        //}
//
        //private async UniTask OnEndAnimation(Action callbacks)
        //{
        //    var delayMultiplayer = 1.2f;
        //    var length = _animator.GetCurrentAnimatorStateInfo(0).length;
        //    await UniTask.Delay((int) (MillisecondsInOneSecond * length * delayMultiplayer));
        //    //_playerMovement.InAction = false;
        //    _animator.applyRootMotion = false;
        //    callbacks?.Invoke();
        //}
    }
}
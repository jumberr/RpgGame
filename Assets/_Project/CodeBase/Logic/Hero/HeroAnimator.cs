using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int Reload = Animator.StringToHash("Reload");
        
        [SerializeField] private Animator _animator;

        public void EnterMoveState(float value) =>
            _animator.SetFloat(Move, value, 0.2f, Time.deltaTime);

        public async UniTask ReloadAnimation(float reloadTime)
        {
            _animator.SetTrigger(Reload);
            var transitionInfo = _animator.GetAnimatorTransitionInfo(0);
            if (transitionInfo.nameHash == Reload) 
                _animator.speed = transitionInfo.duration / reloadTime;
            
            await UniTask.Delay(TimeSpan.FromSeconds(reloadTime));
            ResetAnimatorSpeed();
        }

        private void ResetAnimatorSpeed() => 
            _animator.speed = 1;
    }
}
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Hero
{
    public class PlayerAnimator : MonoBehaviour
    {
        private const int MillisecondsInOneSecond = 1000;
        [SerializeField] private Animator _animator;

        public async void PlayTargetAnimation(int animHash, bool applyRootMotion, Action onComplete = null)
        {
            _animator.applyRootMotion = applyRootMotion;
            _animator.CrossFade(animHash, 0.2f);
            await OnEndAnimation(onComplete);
        }

        private async UniTask OnEndAnimation(Action callbacks)
        {
            var delayMultiplayer = 1.2f;
            var length = _animator.GetCurrentAnimatorStateInfo(0).length;
            await UniTask.Delay((int) (MillisecondsInOneSecond * length * delayMultiplayer));
            //_playerMovement.InAction = false;
            _animator.applyRootMotion = false;
            callbacks?.Invoke();
        }
    }
}
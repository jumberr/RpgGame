using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int Jump = Animator.StringToHash("Jump");

        private static readonly int Reload = Animator.StringToHash("Reload");
        private static readonly int FullReload = Animator.StringToHash("FullReload");
        //private static readonly int Scoping = Animator.StringToHash("Scoping");
        private static readonly int Hide = Animator.StringToHash("Hide");
        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Shoot = Animator.StringToHash("Shoot");
        
        private Animator _animator;

        public void Construct(Animator animator) =>
            _animator = animator;
        
        public void EnterMoveState(float value)
        {
            if (IsAnimatorExists())
                _animator.SetFloat(Move, value, 0.2f, Time.deltaTime);
        }

        public async UniTask ReloadAnimation(float reloadTime) => 
            await ProceedReload(reloadTime, Reload);

        public async UniTask FullReloadAnimation(float reloadTime) => 
            await ProceedReload(reloadTime, FullReload);

        public void ShootAnimation()
        {
            if (IsAnimatorExists())
                _animator.SetTrigger(Shoot);
        }

        //public void Scope(bool value) => 
        //    _animator?.SetBool(Scoping, value);

        public void JumpAnimation()
        {
            if (IsAnimatorExists())
                _animator.SetTrigger(Jump);
        }

        public async UniTask HideWeapon() => 
            await ShowHide(Hide);

        public async UniTask ShowWeapon() => 
            await ShowHide(Show);

        private async Task ProceedReload(float reloadTime, int key)
        {
            if (!IsAnimatorExists()) return;
            _animator.SetTrigger(key);
            await UniTask.Delay(TimeSpan.FromSeconds(reloadTime));
        }
        
        private async UniTask ShowHide(int id)
        {
            _animator.SetTrigger(id);
            var duration = _animator.GetAnimatorTransitionInfo(0).duration;
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
        }

        private bool IsAnimatorExists() => 
            _animator is { };
    }
}
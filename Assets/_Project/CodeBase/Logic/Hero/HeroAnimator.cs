using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        private const int DefaultLayer = 0;
        
        [Header("Animation")]
        [SerializeField] private float _animationThreshold = 0.1f;
        [SerializeField] private float _sprintAnimationValue = 1f;
        [SerializeField] private float _walkAnimationValue = 0.85f;
        [SerializeField] private float _idleAnimationValue;
        
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Reload = Animator.StringToHash("Reload");
        private static readonly int FullReload = Animator.StringToHash("FullReload");
        private static readonly int Hide = Animator.StringToHash("Hide");
        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Shoot = Animator.StringToHash("Shoot");
        private static readonly int EndRevolverReload = Animator.StringToHash("EndRevolverReload");
        private static readonly int PrepareThrowKey = Animator.StringToHash("PrepareThrow");
        private static readonly int ThrowKey = Animator.StringToHash("Throw");
        
        private Animator _animator;

        public void Construct(Animator animator) => 
            _animator = animator;

        public void SetEmptyAnimator() =>
            _animator = null;
        
        public void EnterMoveState(bool isRunning, Vector3 direction)
        {
            if (isRunning)
                EnterMoveState(_sprintAnimationValue);
            else if (direction.magnitude >= _animationThreshold)
                EnterMoveState(_walkAnimationValue);
            else
                EnterMoveState(_idleAnimationValue);
        }
        
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

        public void JumpAnimation()
        {
            if (IsAnimatorExists())
                _animator.SetTrigger(Jump);
        }

        public async UniTask HideWeapon() => 
            await ShowHide(Hide);

        public async UniTask ShowWeapon() => 
            await ShowHide(Show);

        public async UniTask WaitForEndOfTransition() => 
            await UniTask.WaitUntil(() => !IsInTransition());

        public async UniTask ShowWeaponOnInit() => 
            await WaitForCurrentAnimation();

        public void EndRevolverReloadAnimation()
        {
            if (!IsAnimatorExists()) return;
            _animator.SetTrigger(EndRevolverReload);
        }

        public void PrepareThrowing() => 
            _animator.Play(PrepareThrowKey);

        public async UniTask Throw()
        {
            _animator.Play(ThrowKey);
            await WaitForCurrentAnimation();
        }

        private async UniTask ProceedReload(float reloadTime, int key)
        {
            if (!IsAnimatorExists()) return;
            _animator.SetTrigger(key);
            await UniTask.Delay(TimeSpan.FromSeconds(reloadTime));
        }

        private async UniTask ShowHide(int id)
        {
            if (!IsAnimatorExists()) return;
            _animator.SetTrigger(id);
            await WaitForCurrentAnimation();
        }

        private async UniTask WaitForCurrentAnimation()
        {
            var duration = _animator.GetCurrentAnimatorStateInfo(DefaultLayer).length;
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
        }
        
        private bool IsInTransition() => 
            _animator.IsInTransition(DefaultLayer);

        private bool IsAnimatorExists() => 
            _animator is { };
    }
}
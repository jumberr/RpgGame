using System.Threading;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.StaticData.ItemsDataBase.Types;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroScoping : MonoBehaviour
    {
        [SerializeField] private HeroState _state;
        [SerializeField] private InputService _inputService;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Transform _weaponHolder;
        
        private CancellationTokenSource _cts;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _startPos;
        private Vector3 _adsPoint;
        private float _aimingInTime = 0.3f;
        private float _aimingOutTime = 0.3f;
        private float _startFov;
        private float _adsFov = 32f;
        private bool _isScoping;
        private bool _isNeedScope;
        
        public void Construct(Weapon weapon, WeaponConfiguration config)
        {
            _isNeedScope = weapon.WeaponData.Scoping;
            _adsPoint = _weaponHolder.localPosition - _weaponHolder.parent.InverseTransformPoint(config.AdsPoint.transform.position);
        }
        
        public void Construct(Knife knife) => 
            _isNeedScope = knife.Scoping;

        private void OnEnable() => 
            _inputService.OnScope += ScopeHandling;

        private void Start()
        {
            _startPos = _weaponHolder.localPosition;
            _startFov = _mainCamera.fieldOfView;
        }

        private void OnDisable() => 
            _inputService.OnScope -= ScopeHandling;

        public async UniTask UnScope()
        {
            await SmoothTranslation(_startPos, _startFov,_aimingInTime);
            _isScoping = false;
            _state.Enter(PlayerState.None);
        }

        private async void ScopeHandling()
        {
            if (!_isNeedScope) return;
            
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }

            if (!_isScoping)
                await Scope();
            else
                await UnScope();
        }

        private async UniTask Scope()
        {
            if (_state.CurrentPlayerState == PlayerState.Reload) return;
            _state.Enter(PlayerState.Scoping);
            _isScoping = true;
            await SmoothTranslation(_adsPoint, _adsFov, _aimingInTime);
        }

        private async UniTask SmoothTranslation(Vector3 target, float fov, float speed)
        {
            _cts = new CancellationTokenSource();

            while (_weaponHolder.transform.localPosition != target) 
            {
                _weaponHolder.localPosition = Vector3.SmoothDamp(_weaponHolder.localPosition, target, ref _velocity, speed);
                _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, fov, speed * Time.deltaTime);
                if (await UniTask.DelayFrame(1, cancellationToken: _cts.Token).SuppressCancellationThrow())
                    break;
            }        
        }
    }
}
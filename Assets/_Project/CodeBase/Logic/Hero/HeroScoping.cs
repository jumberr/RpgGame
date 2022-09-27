using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.StaticData.ItemsDataBase.Types;
using _Project.CodeBase.StaticData.ItemsDataBase.Types.Attachments;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroScoping : MonoBehaviour
    {
        [SerializeField] private HeroState _state;
        [SerializeField] private InputService _inputService;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Transform _weaponHolder;
        
        private ScopeSettings _scopeSettings;
        private Vector3 _startPos;
        private Vector3 _adsPoint;
        private float _startFov;
        private float _adsFov;
        private bool _isNeedScope;
        private bool _isScoping;
        private TweenerCore<Vector3,Vector3,VectorOptions> _weaponTween;
        private TweenerCore<float,float,FloatOptions> _cameraTween;

        public void Construct(Weapon weapon, WeaponConfiguration config)
        {
            ApplyScopeSettings(weapon);
            _adsPoint = _weaponHolder.localPosition - _weaponHolder.parent.InverseTransformPoint(config.AdsPoint.transform.position);
        }

        public void Construct(Knife knife) => 
            _isNeedScope = false;

        private void OnEnable() => 
            _inputService.OnScope += ScopeHandling;

        private void Start()
        {
            _startPos = _weaponHolder.localPosition;
            _startFov = _mainCamera.fieldOfView;
        }

        private void OnDisable() => 
            _inputService.OnScope -= ScopeHandling;

        public void UnScope()
        {
            SmoothTranslation(_startPos, _startFov, _scopeSettings.AimingOutTime);
            _isScoping = false;
            _state.Enter(PlayerState.None);
        }

        private void ScopeHandling()
        {
            if (!_isNeedScope) return;

            _weaponTween?.Kill();
            _cameraTween?.Kill();
            
            if (!_isScoping)
                Scope();
            else
                UnScope();
        }

        private void Scope()
        {
            if (_state.CurrentPlayerState == PlayerState.Reload) return;
            _state.Enter(PlayerState.Scoping);
            _isScoping = true;
            SmoothTranslation(_adsPoint, _adsFov, _scopeSettings.AimingInTime);
        }

        private void SmoothTranslation(Vector3 target, float fov, float speed)
        {
            _weaponTween = _weaponHolder.transform.DOLocalMove(target, speed)
                .SetEase(Ease.OutQuad);
                
            _cameraTween = _mainCamera.DOFieldOfView(fov, speed)
                .SetEase(Ease.OutQuad);
        }

        private void ApplyScopeSettings(Weapon weapon)
        {
            _isNeedScope = weapon.WeaponData.Scoping;
            _scopeSettings = weapon.DefaultScopeData;
            _adsFov = _startFov / _scopeSettings.ScopeMultiplier;
        }
    }
}
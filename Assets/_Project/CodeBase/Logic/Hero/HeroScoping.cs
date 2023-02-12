using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.StaticData;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroScoping : MonoBehaviour
    {
        [SerializeField] private HeroState _state;
        [SerializeField] private Camera _weaponCamera;
        [SerializeField] private Transform _weaponHolder;

        private InputService _inputService;
        private ScopeSpecs _scopeSpecs;
        private WeaponConfiguration _config;
        private Vector3 _startPos;
        private Vector3 _adsPoint;
        private float _startFov;
        private float _adsFov;
        private bool _isNeedScope;
        private TweenerCore<Vector3, Vector3, VectorOptions> _weaponTween;
        private TweenerCore<float, float, FloatOptions> _weaponCameraTween;

        public void Construct(GunInfo gunInfo, WeaponConfiguration config)
        {
            _config = config;
            ApplyScopeSettings(gunInfo);
            _adsPoint = _weaponHolder.localPosition - _weaponHolder.parent.InverseTransformPoint(config.AdsPoint.transform.position);
        }

        public void Construct(KnifeInfo knifeInfo) =>
            _isNeedScope = false;

        private void Start()
        {
            _startPos = _weaponHolder.localPosition;
            _startFov = _weaponCamera.fieldOfView;
        }

        private void OnDisable() =>
            _inputService.ScopeAction.Event -= ScopeHandling;

        public void SetInputService(InputService inputService)
        {
            _inputService = inputService;
            _inputService.ScopeAction.Event += ScopeHandling;
        }

        public void UnScope()
        {
            TurnScopeRender(false, _scopeSpecs.AimingOutTime);
            SmoothTranslation(_startPos, _startFov, _scopeSpecs.AimingOutTime);
            _state.Aiming = false;
        }

        private void ScopeHandling()
        {
            if (!_isNeedScope) return;

            KillTween();

            if (!_state.Aiming)
                Scope();
            else
                UnScope();
        }

        private void Scope()
        {
            if (_state.Reloading) return;
            _state.Aiming = true;
            TurnScopeRender(true, _scopeSpecs.AimingInTime);
            SmoothTranslation(_adsPoint, _adsFov, _scopeSpecs.AimingInTime);
        }

        private void SmoothTranslation(Vector3 target, float fov, float speed)
        {
            _weaponTween = _weaponHolder.transform.DOLocalMove(target, speed)
                .SetEase(Ease.OutQuad);

            _weaponCameraTween = _weaponCamera.DOFieldOfView(fov, speed)
                .SetEase(Ease.OutQuad);
        }

        private void ApplyScopeSettings(GunInfo gunInfo)
        {
            _isNeedScope = gunInfo.GunSpecs.Scoping;
            _scopeSpecs = gunInfo.DefaultSettings.ScopeSpecs;
            _adsFov = _startFov / _scopeSpecs.ScopeMultiplier;
        }

        private void TurnScopeRender(bool value, float time)
        {
            if (_config.ScopeRender != null)
            {
                _config.ScopeRender.gameObject.SetActive(value);
                _config.ScopeRender.Turn(value, time);
            }
        }

        private void KillTween()
        {
            _weaponTween?.Kill();
            _weaponCameraTween?.Kill();
        }
    }
}
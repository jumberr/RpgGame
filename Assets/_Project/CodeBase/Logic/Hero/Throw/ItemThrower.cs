using System;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using NTC.Global.Pool;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Hero
{
    public class ItemThrower : IInitializable, IDisposable
    {
        private readonly IGameFactory _gameFactory;
        private readonly TrajectoryDrawer _trajectoryDrawer;
        private readonly InputService _inputService;

        private ActiveItemController _activeItemController;
        private Transform _mainCamera;
        private GrenadeInfo _grenadeInfo;
        private GrenadeView _projectile;
        private GrenadeIKController _ikController;
        private UniTask _chargeThrowTask;
        private bool _charge;
        private float _chargeTime;

        private Vector3 CameraForward => _mainCamera.transform.forward;
        private float Force => Mathf.Min(_chargeTime * _grenadeInfo.ThrowData.ThrowForce, _grenadeInfo.ThrowData.MaxForce);
        private Vector3 ThrowDirection { get; } = new Vector3(0, 1, 0);


        public ItemThrower(IGameFactory gameFactory, TrajectoryDrawer trajectoryDrawer, InputService inputService)
        {
            _gameFactory = gameFactory;
            _trajectoryDrawer = trajectoryDrawer;
            _inputService = inputService;
            
            _gameFactory.HeroCreated += HeroCreated;
        }

        public void Initialize() => 
            _inputService.ScopeAction.Event += HandleThrowing;

        public void Dispose()
        {
            _inputService.ScopeAction.Event -= HandleThrowing;
            _activeItemController.OnSwitch -= ItemSwitched;
        }

        private void HeroCreated(HeroFacade heroFacade)
        {
            _gameFactory.HeroCreated -= HeroCreated;
            _activeItemController = heroFacade.ActiveItemController;
            _mainCamera = heroFacade.CameraTransform;
            
            _activeItemController.OnSwitch += ItemSwitched;
        }

        private void ItemSwitched(bool value)
        {
            if (!value) return;
            var item = _activeItemController.CurrentItem;

            if (item != null && item.Info is GrenadeInfo grenadeInfo)
            {
                _grenadeInfo = grenadeInfo;
                item.GameObject.TryGetComponent(out _ikController);
                _projectile = NightPool.Spawn(grenadeInfo.GrenadeProjectile, _ikController.SpawnPosition, Quaternion.identity);
                TogglePhysics(false);
                //item.GameObject.TryGetComponent(out _throwView);
            }
            else
            {
                _grenadeInfo = null;
                if (_projectile != null)
                    NightPool.Despawn(_projectile);
            }
        }

        private void HandleThrowing(bool value)
        {
            if (_grenadeInfo is null) return;

            if (value)
                ChargeThrow();
            else
                ReleaseThrow();
        }

        private async void ChargeThrow()
        {
            _charge = true;
            
            while (_charge)
            {
                var deltaTime = Time.deltaTime;
                
                await UniTask.Delay(TimeSpan.FromSeconds(deltaTime));
                _chargeTime += deltaTime;

                var velocity = GetNormalizedThrowDirection() * Force;
                _trajectoryDrawer.ShowTrajectory(_ikController.ThrowPositionForward, velocity);
                
                // todo update charge progress bar ui
            }
        }

        private void ReleaseThrow()
        {
            _charge = false;
            
            // if (ChargedEnough)
            ThrowItem(Force).Forget();
            _chargeTime = 0f;
            _trajectoryDrawer.ToggleTrajectory(false);
        }

        private async UniTask ThrowItem(float force)
        {
            //var spawnPosition = _projectile.ThrowView.ThrowPosition.position + CameraForward;
            var throwDirection = GetNormalizedThrowDirection();
            
            TogglePhysics(true);
            _projectile.Rigidbody.AddForce(throwDirection * force, ForceMode.VelocityChange);
            await _projectile.Throw(_grenadeInfo as ExplosiveGrenadeInfo); // todo ExplosiveGrenadeInfo can be null
            NightPool.Despawn(_projectile);
        }

        private Vector3 GetNormalizedThrowDirection() => 
            (CameraForward + ThrowDirection).normalized;
        
        private void TogglePhysics(bool value)
        {
            ToggleIKConstraints(value);
            _projectile.Rigidbody.useGravity = value;
        }

        private void ToggleIKConstraints(bool value)
        {
            if (value)
                _ikController.Enable(_projectile.IKTargets);
            else
                _ikController.Disable();
        }
    }
}
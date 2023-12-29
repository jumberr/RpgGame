using System;
using System.Threading;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Inventory;
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
        private HeroInventory _inventory;
        private HeroAnimator _animator;
        private Transform _mainCamera;
        private GrenadeInfo _grenadeInfo;
        private GrenadeView _projectile;
        private GrenadeIKController _ikController;
        private CancellationTokenSource _cts;
        private float _chargeTime;

        private Vector3 CameraForward => _mainCamera.transform.forward;
        private float Force => Mathf.Min(_chargeTime * _grenadeInfo.ThrowData.ThrowForce, _grenadeInfo.ThrowData.MaxForce);


        public ItemThrower(
            IGameFactory gameFactory,
            TrajectoryDrawer trajectoryDrawer,
            InputService inputService)
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
            
            Cleanup();
        }

        private void HeroCreated(HeroFacade heroFacade)
        {
            _gameFactory.HeroCreated -= HeroCreated;
            _activeItemController = heroFacade.ActiveItemController;
            _inventory = heroFacade.Inventory;
            _animator = heroFacade.Animator;
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
                SpawnProjectile(grenadeInfo);
            }
            else
                ClearInfo();
        }

        private void HandleThrowing(bool value)
        {
            if (_grenadeInfo is null) return;

            if (value)
                ChargeThrow();
            else
                ReleaseThrow().Forget();
        }

        private async void ChargeThrow()
        {
            _cts = new CancellationTokenSource();
            
            while (Application.isPlaying)
            {
                var deltaTime = Time.deltaTime;

                if (await UniTask.Delay(TimeSpan.FromSeconds(deltaTime), cancellationToken: _cts.Token)
                        .SuppressCancellationThrow()) break;

                _chargeTime += deltaTime;

                var velocity = GetNormalizedThrowDirection() * Force;
                _trajectoryDrawer.ShowTrajectory(_ikController.SpawnPosition.position, velocity,Force / _grenadeInfo.ThrowData.MaxForce);
            }
        }

        private async UniTask ReleaseThrow()
        {
            Cleanup();

            _inventory.RemoveItemFromSlot(_grenadeInfo.UIInfo.Name);
            ThrowItem(_projectile, Force).Forget();
            
            _chargeTime = 0f;
            _trajectoryDrawer.ToggleTrajectory(false);

            await _animator.HideWeapon();
            await ResupplyNewItem();
        }

        private async UniTask ResupplyNewItem()
        {
            if (_inventory.FindItemAmount(_grenadeInfo.UIInfo.Name) > 0)
            {
                await _animator.ShowWeapon();
                SpawnProjectile(_grenadeInfo);
            }
            else
                ClearInfo();
        }

        private async UniTask ThrowItem(GrenadeView projectile, float force)
        {
            var throwDirection = GetNormalizedThrowDirection();
            
            TogglePhysics(projectile, true);
            projectile.Rigidbody.AddForce(throwDirection * force, ForceMode.VelocityChange);
            await projectile.Throw(_grenadeInfo as ExplosiveGrenadeInfo); // todo ExplosiveGrenadeInfo can be null
            DespawnProjectile(projectile);
        }

        private void SpawnProjectile(GrenadeInfo grenadeInfo)
        {
            _projectile = NightPool.Spawn(grenadeInfo.GrenadeProjectile, _ikController.SpawnPosition, Quaternion.identity);
            _projectile.ResetTransform();
            TogglePhysics(_projectile, false);
        }

        private void ClearInfo() => 
            _grenadeInfo = null;

        private void DespawnProjectile(GrenadeView projectile)
        {
            if (projectile != null)
                NightPool.Despawn(projectile);
        }

        private void TogglePhysics(GrenadeView projectile, bool value)
        {
            ToggleIKConstraints(projectile, !value);
            projectile.TogglePhysics(value);
        }

        private void ToggleIKConstraints(GrenadeView projectile, bool value)
        {
            if (value)
                _ikController.Enable(projectile.IKTargets);
            else
                _ikController.Disable();
        }

        private Vector3 GetNormalizedThrowDirection() => 
            (CameraForward + Vector3.up).normalized;

        private void Cleanup()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}
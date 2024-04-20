using System;
using System.Threading;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services;
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
        private readonly SoundService _soundService;

        private ActiveItemController _activeItemController;
        private HeroInventory _inventory;
        private HeroAnimator _animator;
        private Transform _mainCamera;
        private GrenadeInfo _grenadeInfo;
        private GrenadeSpecs _grenadeSpecs;
        private GrenadeView _projectile;
        private GrenadeIKController _ikController;
        private CancellationTokenSource _cts;
        private float _chargeTime;

        private Vector3 CameraForward => _mainCamera.transform.forward;


        public ItemThrower(
            IGameFactory gameFactory,
            TrajectoryDrawer trajectoryDrawer,
            InputService inputService,
            SoundService soundService)
        {
            _gameFactory = gameFactory;
            _trajectoryDrawer = trajectoryDrawer;
            _inputService = inputService;
            _soundService = soundService;

            _gameFactory.HeroCreated += HeroCreated;
        }

        public void Initialize() => 
            _inputService.ScopeAction.Event += HandleThrowing;

        public void Dispose()
        {
            _inputService.ScopeAction.Event -= HandleThrowing;
            if (_activeItemController != null)
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
                _grenadeSpecs = _grenadeInfo.GetSpecs();
                item.GameObject.TryGetComponent(out _ikController);
                SpawnProjectile();
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
            _soundService.PlaySound(_grenadeSpecs.PullPinSoundData).Forget();
            
            while (Application.isPlaying)
            {
                var deltaTime = Time.deltaTime;

                if (await UniTask.Delay(TimeSpan.FromSeconds(deltaTime), cancellationToken: _cts.Token).SuppressCancellationThrow()) break;

                _chargeTime += deltaTime;
                _trajectoryDrawer.ShowTrajectory(_ikController.SpawnPosition.position, GetForceDirection(),GetForce() / _grenadeSpecs.ThrowData.MaxForce);
            }
        }

        private async UniTask ReleaseThrow()
        {
            Cleanup();

            _inventory.RemoveItemFromSlot(_grenadeInfo.UIInfo.Name);
            ThrowItem(_projectile).Forget();
            
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
                SpawnProjectile();
            }
            else
                ClearInfo();
        }

        private async UniTask ThrowItem(GrenadeView projectile)
        {
            projectile.UnparentGrenade();
            TogglePhysics(projectile, true);
            _soundService.PlaySound(_grenadeSpecs.ThrowData.ThrowSoundData).Forget();
            
            await projectile.Throw(_grenadeInfo as ExplosiveGrenadeInfo, GetForceDirection());
            DespawnProjectile(projectile);
        }

        private void SpawnProjectile()
        {
            _projectile = NightPool.Spawn(_grenadeSpecs.GrenadeProjectile, _ikController.SpawnPosition, Quaternion.identity);
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
            if (value)
            {
                ToggleIKConstraints(projectile, false);
                projectile.TogglePhysics(true);
            }
            else
            {
                projectile.TogglePhysics(false);
                ToggleIKConstraints(projectile, true);
            }
        }

        private void ToggleIKConstraints(GrenadeView projectile, bool value)
        {
            if (value)
                _ikController.Enable(projectile.IKTargets);
            else
                _ikController.Disable();
        }

        private Vector3 GetForceDirection() => 
            GetForce() * (CameraForward + Vector3.up).normalized;

        private void Cleanup()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
        
        private float GetForce() => 
            Mathf.Min(_chargeTime * _grenadeSpecs.ThrowData.ThrowForce, _grenadeSpecs.ThrowData.MaxForce);
    }
}
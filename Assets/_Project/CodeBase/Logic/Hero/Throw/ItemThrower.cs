using System;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Hero
{
    public class ItemThrower : IInitializable, IDisposable
    {
        private readonly IGameFactory _gameFactory;
        private readonly TrajectoryDrawer _trajectoryDrawer;
        private readonly InputService _inputService;
        private readonly Vector3 _throwDirection = new Vector3();

        private ActiveItemController _activeItemController;
        private Transform _mainCamera;
        private Transform _throwPosition; // todo???
        private ThrowData _throwData;
        private UniTask _chargeThrowTask;
        private bool _charge;
        private float _chargeTime;

        private Vector3 CameraForward => _mainCamera.transform.forward;


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
                _throwData = grenadeInfo.ThrowData;
            else
                _throwData = null;
        }

        private void HandleThrowing(bool value)
        {
            if (_throwData is null) return;

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
                _chargeTime += deltaTime;                 // todo maybe add clamp
                
                var velocity = GetNormalizedThrowDirection() * Mathf.Min(_chargeTime * _throwData.ThrowForce, _throwData.MaxForce); 
                _trajectoryDrawer.ShowTrajectory(_throwPosition.position + _throwPosition.forward, velocity);
                
                // todo update charge progress bar ui
            }
        }

        private void ReleaseThrow()
        {
            _charge = false;
            
            // if (ChargedEnough)
            ThrowItem();
            _chargeTime = 0f;
            _trajectoryDrawer.HideTrajectory();
        }

        private void ThrowItem()
        {
            var spawnPos = _throwPosition.position + CameraForward;
            
            // todo spawn item
            var throwDirection = GetNormalizedThrowDirection();
            // todo add force
        }

        private Vector3 GetNormalizedThrowDirection() => 
            (CameraForward + _throwDirection).normalized;
    }
}
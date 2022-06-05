using System;
using System.Collections.Generic;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.HeroWeapon.Animations;
using _Project.CodeBase.StaticData.ItemsDataBase.Types;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Reload
{
    public class ReloadRevolver : IReloadSystem
    {
        private readonly HeroAnimator _heroAnimator;
        private RevolverAnimation _revolverAnimation;
        private List<GameObject> _revolverAmmo;

        private int _revolverRestoredAmmo;
        private bool _subscribed;

        private float _startReloadAnimation;
        private float _oneAmmoReload;
        private float _endReloadAnimation;

        public ReloadRevolver(HeroAnimator heroAnimator) => 
            _heroAnimator = heroAnimator;

        public void Construct(Weapon weapon, WeaponConfiguration config, RevolverAnimation revolverAnimation)
        {
            if (_subscribed)
                UnsubscribeRevolverEvents();

            _revolverAmmo = config.RevolverAmmo;
            _revolverAnimation = revolverAnimation;
            SubscribeRevolverEvents();

            var revolver = weapon as Revolver;
            _startReloadAnimation = revolver.StartReloadDuration;
            _oneAmmoReload = revolver.OneAmmoReloadDuration;
            _endReloadAnimation = revolver.EndReloadAnimation;
        }

        public async UniTask Reload((ReloadState, int, int) result)
        {
            for (var i = 0; i < result.Item2; i++) 
                _revolverAmmo[i].SetActive(false);
            _revolverRestoredAmmo = result.Item3;
            var time = _startReloadAnimation + _revolverRestoredAmmo * _oneAmmoReload;
            await RevolverReloadAnimation(time);
        }

        public void UnsubscribeRevolverEvents()
        {
            if (_revolverAnimation is null) return;
            _revolverAnimation.AmmoAdded -= OnAmmoAdded;
            _revolverAnimation.AmmoEnabled -= OnAmmoEnabled;
            _subscribed = false;
        }

        private void SubscribeRevolverEvents()
        {
            _revolverAnimation.AmmoAdded += OnAmmoAdded;
            _revolverAnimation.AmmoEnabled += OnAmmoEnabled;
            _subscribed = true;
        }
        
        private async UniTask RevolverReloadAnimation(float time)
        {
            await _heroAnimator.ReloadAnimation(time);
            await UniTask.Delay(TimeSpan.FromSeconds(_endReloadAnimation));
        }

        // Revolver Animation Callbacks
        private void OnAmmoEnabled(int ammoIndex)
        {
            if (ammoIndex < _revolverRestoredAmmo)
                _revolverAmmo[ammoIndex].SetActive(true);
        }

        private void OnAmmoAdded(int ammoIndex)
        {
            if (ammoIndex != _revolverRestoredAmmo - 1) return;
            _heroAnimator.EndRevolverReloadAnimation();
            _revolverRestoredAmmo = 0;
        }
    }
}
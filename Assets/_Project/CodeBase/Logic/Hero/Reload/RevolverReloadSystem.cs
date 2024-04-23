using System;
using System.Collections.Generic;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.HeroWeapon.Animations;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Reload
{
    public class RevolverReloadSystem : BaseReloadSystem, IDisposable
    {
        private RevolverAnimation _revolverAnimation;
        private List<GameObject> _revolverAmmo;
        private RevolverSpecs _revolverInfo;

        private int _revolverRestoredAmmo;
        private bool _subscribed;
        
        
        public void Dispose() => 
            UnsubscribeRevolverEvents();

        public override bool Setup<T>(WeaponInfo<T> info, WeaponConfiguration config)
        {
            if (!ItemInfoUtils.IsRevolver(info, out var revolverInfo)) return false;
            
            Setup(revolverInfo, config);
            return true;
        }

        public override async UniTask Reload((ReloadState, int, int) result)
        {
            for (var i = 0; i < result.Item2; i++) 
                _revolverAmmo[i].SetActive(false);
            _revolverRestoredAmmo = result.Item3;
            var time = _revolverInfo.StartReloadDuration + _revolverRestoredAmmo * _revolverInfo.OneAmmoReloadDuration;
            await RevolverReloadAnimation(time);
        }
        
        private void Setup(WeaponInfo<RevolverSpecs> revolverInfo, WeaponConfiguration config)
        {
            if (_subscribed)
                UnsubscribeRevolverEvents();

            _revolverAmmo = config.RevolverAmmo;
            _revolverAnimation = config.RevolverAnimation;
            _revolverInfo = revolverInfo.GetSpecs();

            SubscribeRevolverEvents();
        }

        private void UnsubscribeRevolverEvents()
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
            await ReloadAnimation(time);
            await UniTask.Delay(TimeSpan.FromSeconds(_revolverInfo.EndReloadAnimation));
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
            HeroAnimator.EndRevolverReloadAnimation();
            _revolverRestoredAmmo = 0;
        }
    }
}
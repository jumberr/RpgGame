﻿using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Logic.Hero.Reload
{
    public class DefaultReload : IReloadSystem
    {
        private readonly HeroAnimator _heroAnimator;
        
        private float _reloadTime;
        private float _fullReloadTime;

        public DefaultReload(HeroAnimator heroAnimator) => 
            _heroAnimator = heroAnimator;

        public void Construct(GunInfo gunInfo)
        {
            _reloadTime = gunInfo.GunSpecs.ReloadTime;
            _fullReloadTime = gunInfo.GunSpecs.FullReloadTime;
        }

        public async UniTask Reload((ReloadState, int, int) result)
        {
            if (result.Item1 == ReloadState.FullReload)
                await FullReloadAnimation();
            else if (result.Item1 == ReloadState.Reload)
                await ReloadAnimation();
        }
        
        private async UniTask FullReloadAnimation() => 
            await _heroAnimator.FullReloadAnimation(_fullReloadTime);

        private async UniTask ReloadAnimation() => 
            await _heroAnimator.ReloadAnimation(_reloadTime);
    }
}
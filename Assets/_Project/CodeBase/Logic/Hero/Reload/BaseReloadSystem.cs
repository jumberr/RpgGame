using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Logic.Hero.Reload
{
    public abstract class BaseReloadSystem : IReloadSystem
    {
        protected HeroAnimator HeroAnimator;

        
        public void Init(HeroAnimator heroAnimator) => 
            HeroAnimator = heroAnimator;

        public abstract bool Setup<T>(WeaponInfo<T> info, WeaponConfiguration config) where T : WeaponSpecs;
        public abstract UniTask Reload((ReloadState, int, int) result);
        
        protected async UniTask ReloadAnimation(float time) => 
            await HeroAnimator.ReloadAnimation(time);
    }
}
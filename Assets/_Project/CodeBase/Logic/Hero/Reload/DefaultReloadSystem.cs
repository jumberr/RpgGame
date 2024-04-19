using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.Utils;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Logic.Hero.Reload
{
    public class DefaultReloadSystem : BaseReloadSystem
    {
        private GunSpecs _gunSpecs;

        
        public override bool Setup<T>(WeaponInfo<T> info, WeaponConfiguration config)
        {
            if (!ItemInfoUtils.IsGun(info, out var gunInfo)) return false;
                
            _gunSpecs = gunInfo.GetSpecs();
            return true;
        }

        public override async UniTask Reload((ReloadState, int, int) result)
        {
            if (result.Item1 == ReloadState.FullReload)
                await FullReloadAnimation();
            else if (result.Item1 == ReloadState.Reload)
                await ReloadAnimation(_gunSpecs.ReloadTime);
        }
        
        private async UniTask FullReloadAnimation() => 
            await HeroAnimator.FullReloadAnimation(_gunSpecs.FullReloadTime);
    }
}
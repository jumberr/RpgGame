using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Logic.Hero.Reload
{
    public interface IReloadSystem
    {
        void Init(HeroAnimator heroAnimator);
        bool Setup<T>(WeaponInfo<T> info, WeaponConfiguration config) where T : WeaponSpecs;
        UniTask Reload((ReloadState, int, int) result);
    }
}
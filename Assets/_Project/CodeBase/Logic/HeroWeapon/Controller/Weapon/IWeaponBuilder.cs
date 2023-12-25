using _Project.CodeBase.Logic.Hero.Shooting;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public interface IWeaponBuilder : IItemManipulator
    {
        WeaponType Type { get; }
        UniTask AnimatorHideWeapon();
        void FinishItemDestroy();
    }
}
using _Project.CodeBase.Utils;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public interface IWeaponBuilder : IItemManipulator, ITypeRestricted
    {
        UniTask AnimatorHideWeapon();
        void FinishItemDestroy();
    }
}
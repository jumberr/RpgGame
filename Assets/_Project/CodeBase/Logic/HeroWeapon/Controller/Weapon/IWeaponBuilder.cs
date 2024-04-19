using System;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public interface IWeaponBuilder : IItemManipulator
    {
        Type SupportedType { get; }
        UniTask AnimatorHideWeapon();
        void FinishItemDestroy();
    }
}
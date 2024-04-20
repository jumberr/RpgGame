using System;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public interface IWeaponBuilder : IItemManipulator
    {
        Type[] SupportedTypes { get; }
        UniTask AnimatorHideWeapon();
        void FinishItemDestroy();
    }
}
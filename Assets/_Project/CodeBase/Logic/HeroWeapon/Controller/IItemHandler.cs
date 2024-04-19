using System;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public interface IItemHandler : IItemManipulator
    {
        public Type[] SupportedTypes { get; }
        void InventoryUpdate();
        UniTask Destroy();
    }
}
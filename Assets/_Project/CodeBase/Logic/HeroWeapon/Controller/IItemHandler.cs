using _Project.CodeBase.Logic.Inventory;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public interface IItemHandler : IItemManipulator
    {
        public ItemType ItemType { get; }
        void InventoryUpdate();
        UniTask Destroy();
    }
}
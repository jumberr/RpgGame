using _Project.CodeBase.Utils;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public interface IItemHandler : IItemManipulator, ITypeRestricted
    {
        void InventoryUpdate();
        UniTask Destroy();
    }
}
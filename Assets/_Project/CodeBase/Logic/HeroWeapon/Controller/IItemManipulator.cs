using _Project.CodeBase.Data;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.HeroWeapon.Data;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public interface IItemManipulator
    {
        public CurrentItem CurrentItem { get; }
        public void Init(HeroFacade facade, ItemsInfo itemsInfo, Transform parent);
        UniTask CreateNewItem(int slotID, CommonItemPart part, ItemInfo info, GameObject prefab);
    }
}
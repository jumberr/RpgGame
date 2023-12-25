using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Logic.HeroWeapon.Data;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    // todo: implement when food will be in priority
    public class FoodItemHandler : BaseItemHandler
    {
        public override CurrentItem CurrentItem { get; } = new CurrentItem();
        public override ItemType ItemType => ItemType.Food;
        
        public override UniTask CreateNewItem(int slotID, CommonItemPart part, ItemInfo info, GameObject prefab)
        {
            throw new Exception();
        }

        protected override UniTask DestroyItem()
        {
            throw new Exception();
        }
    }
}
using System;
using _Project.CodeBase.Logic;
using _Project.CodeBase.Logic.Inventory;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class CommonItemPart
    {
        public int DbId;
        public int Amount;
        public BaseItem Item;

        public CommonItemPart() => 
            DbId = Inventory.ErrorIndex;

        public CommonItemPart(int dbId, int amount, BaseItem item)
        {
            DbId = dbId;
            Amount = amount;
            Item = item;
        }
    }
}
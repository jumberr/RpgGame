using System;
using _Project.CodeBase.Logic;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class ItemData
    {
        public CommonItemPart CommonItemPart;
        public GameObjectData GameObjectData;

        public ItemData()
        {
            CommonItemPart = new CommonItemPart();
            GameObjectData = new GameObjectData();
        }

        public ItemData(int dbId, int amount, BaseItem item, GameObjectData gameObjectData)
        {
            CommonItemPart = new CommonItemPart(dbId, amount, item);
            GameObjectData = gameObjectData;
        }
    }
}
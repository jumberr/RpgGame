using _Project.CodeBase.StaticData;
using UnityEngine;

namespace _Project.CodeBase.Logic.Inventory
{
    public class ItemGround : MonoBehaviour
    {
        public int Amount;
        [SerializeField] private ItemName _itemName;
        private BaseItem _item;
        private ItemsInfo _itemsInfo;

        public BaseItem Item => _item;
        public int DbID { get; private set; } = -1;

        public void Construct(ItemsInfo itemsInfo, BaseItem data, int amount)
        {
            _itemsInfo = itemsInfo;
            DbID = TryConvertNameToId(_itemName);
            UpdateAmount(amount);
            SetItemRuntimeData(data);
        }

        public void UpdateAmount(int amount) => 
            Amount = amount;

        private void SetItemRuntimeData(BaseItem data) => 
            _item = data ?? CreateDataByID(DbID);

        private int TryConvertNameToId(ItemName itemName)
        {
            if (itemName == ItemName.None) return Inventory.ErrorIndex;
            
            var findItem = _itemsInfo.FindItem(itemName);
            return findItem.PayloadInfo.DbId;
        }
        
        private BaseItem CreateDataByID(int id)
        {
            BaseItem data;
            var type = _itemsInfo.FindItem(id).PayloadInfo.ItemType;
            if (type == ItemType.Weapon)
                data = new WeaponItem(new MagazineData(MagazineData.Empty), new WeaponData());
            else
                data = new DefaultItem();
            return data;
        }
    }
}
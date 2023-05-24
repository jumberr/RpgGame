using System;
using System.Linq;
using _Project.CodeBase.StaticData;

namespace _Project.CodeBase.Logic.Inventory
{
    [Serializable]
    public class Inventory
    {
        public const int ErrorIndex = -1;
        
        public InventorySlot[] Slots;
        public int HotBarSlots;

        public Inventory(InventoryData inventoryData)
        {
            InitializeInventory(inventoryData.InventorySize);
            HotBarSlots = inventoryData.HotBarSize;
        }

        public void InitializeInventory(int slotAmount)
        {
            Slots = new InventorySlot[slotAmount];
            for (var i = 0; i < Slots.Length; i++) 
                Slots[i] = new InventorySlot();
        }

        public void AddItemToInventory(int dbId, ItemPayloadInfo item,  int amount) => 
            AddItemWithReturnAmount(dbId, item, amount);

        public int AddItemWithReturnAmount(int dbId, ItemPayloadInfo item,  int amount)
        {
            while (amount > 0)
            {
                var id = FindSlotOrEmpty(dbId);
                if (id != ErrorIndex)
                {
                    var part = Slots[id].CommonItemPart;
                    part.DbId = dbId;
                    var startAmount = part.Amount;
                    var maxItems = item.MaxInContainer;

                    var diff = maxItems - startAmount;
                    if (diff > amount)
                    {
                        Slots[id].State = SlotState.Middle;
                        part.Amount += amount;
                        amount = 0;
                        return amount;
                    }

                    Slots[id].State = SlotState.Full;
                    part.Amount += diff;
                    amount -= diff;
                }
                else
                    return amount;
            }
            return amount;
        }
        
        public void RemoveItemFromInventory(int dbId,  int amount)
        {
            while (amount > 0)
            {
                var id = FindSlotReversed(dbId);
                if (id != ErrorIndex)
                {
                    var part = Slots[id].CommonItemPart;
                    var startAmount = part.Amount;
                    if (startAmount > amount)
                    {
                        part.Amount -= amount;
                        Slots[id].State = SlotState.Middle;
                        return;
                    }

                    amount -= part.Amount;
                    part.Amount = 0;
                    part.DbId = ErrorIndex;
                    Slots[id].State = SlotState.Empty;
                }
                else
                    return; 
            }
        }

        public void RemoveItemFromSlot(int id)
        {
            if (Slots.Length <= id || Slots[id].State == SlotState.Empty) return;
            
            if (Slots[id].CommonItemPart.Amount <= 1) 
                RemoveAllItemsFromSlot(id);
            else
                Slots[id].CommonItemPart.Amount -= 1;
        }

        public void SwapSlots(int one, int two) => 
            (Slots[one], Slots[two]) = (Slots[two], Slots[one]);

        public void RemoveAllItemsFromSlot(int id)
        {
            if (Slots.Length <= id || Slots[id].State == SlotState.Empty) return;
            var part = Slots[id].CommonItemPart;
            part.DbId = ErrorIndex;
            part.Amount = 0;
            Slots[id].State = SlotState.Empty;
        }

        public InventorySlot FindSlot(int id, int amount) => 
            Slots.FirstOrDefault(slot => slot.ID == id && slot.CommonItemPart.Amount == amount);

        private int FindSlotOrEmpty(int dbId)
        {
            for (var i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].ID == dbId && Slots[i].State == SlotState.Middle)
                    return i;
            }

            return FindEmptySlot();
        }

        private int FindSlotReversed(int dbId)
        {
            for (var i = Slots.Length - 1; i >= 0; i--)
            {
                if (Slots[i].ID == dbId && Slots[i].State != SlotState.Empty)
                    return i;
            }
            return ErrorIndex;
        }

        private int FindEmptySlot()
        {
            for (var i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].State == SlotState.Empty)
                    return i;
            }
            return ErrorIndex;
        }
    }
}
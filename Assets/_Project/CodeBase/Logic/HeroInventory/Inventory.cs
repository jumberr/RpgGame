using System;
using _Project.CodeBase.StaticData.ItemsDataBase;

namespace _Project.CodeBase.Logic.HeroInventory
{
    [Serializable]
    public class Inventory
    {
        public InventorySlot[] Slots;

        public Inventory(int amount) => 
            InitializeInventory(amount);

        public void InitializeInventory(int amount)
        {
            Slots = new InventorySlot[amount];
            for (var i = 0; i < Slots.Length; i++)
            {
                Slots[i] = new InventorySlot
                {
                    DbId = -1,
                    State = SlotState.Empty
                };
            }
        }

        public void AddItemToInventory(int dbId, ItemPayloadData item,  int amount)
        {
            while (amount > 0)
            {
                var id = FindSlotOrEmpty(dbId);
                if (id != -1)
                {
                    Slots[id].DbId = dbId;
                    var startAmount = Slots[id].Amount;
                    var maxItems = item.MaxInContainer;

                    var diff = maxItems - startAmount;
                    if (diff > amount)
                    {
                        Slots[id].State = SlotState.Middle;
                        Slots[id].Amount += amount;
                        return;
                    }

                    Slots[id].State = SlotState.Full;
                    Slots[id].Amount += diff;
                    amount -= diff;
                }
                else
                    return; 
            }
        }
        
        public void RemoveItemFromInventory(int dbId,  int amount)
        {
            while (amount > 0)
            {
                var id = FindSlotReversed(dbId);
                if (id != -1)
                {
                    var startAmount = Slots[id].Amount;
                    if (startAmount > amount)
                    {
                        Slots[id].Amount -= amount;
                        Slots[id].State = SlotState.Middle;
                        return;
                    }

                    amount -= Slots[id].Amount;
                    Slots[id].Amount = 0;
                    Slots[id].DbId = -1;
                    Slots[id].State = SlotState.Empty;
                }
                else
                    return; 
            }
        }

        public void RemoveItemFromSlot(int id)
        {
            if (Slots.Length <= id || Slots[id].State == SlotState.Empty) return;
            
            if (Slots[id].Amount <= 1) 
                RemoveAllItemsFromSlot(id);
            else
                Slots[id].Amount -= 1;
        }
        
        public void RemoveAllItemsFromSlot(int id)
        {
            if (Slots.Length <= id || Slots[id].State == SlotState.Empty) return;
            Slots[id].DbId = -1;
            Slots[id].State = SlotState.Empty;
            Slots[id].Amount = 0;
        }

        private int FindSlotOrEmpty(int dbId)
        {
            for (var i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].DbId == dbId && Slots[i].State == SlotState.Middle)
                    return i;
            }

            return FindEmptySlot();
        }
        
        private int FindSlotReversed(int dbId)
        {
            for (var i = Slots.Length - 1; i >= 0; i--)
            {
                if (Slots[i].DbId == dbId && Slots[i].State != SlotState.Empty)
                    return i;
            }
            return -1;
        }

        private int FindEmptySlot()
        {
            for (var i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].State == SlotState.Empty)
                    return i;
            }
            return -1;
        }
    }
}
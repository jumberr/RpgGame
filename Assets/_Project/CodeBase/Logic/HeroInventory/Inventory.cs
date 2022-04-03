using System;
using System.Linq;
using _Project.CodeBase.StaticData.ItemsDataBase;

namespace _Project.CodeBase.Logic.HeroInventory
{
    [Serializable]
    public class Inventory
    {
        public InventorySlot[] Slots;

        public Inventory(int amount) => 
            InitializeInventory(amount);

        //public bool FreeSlotExists => Array.FindIndex(Slots, x => !x.IsFilled) >= 0;
        //public int FreeSlotIndex => Array.FindIndex(Slots, x => !x.IsFilled);

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

        //public bool IsExistsItemInInventory(int dbID, out int id)
        //{
        //    id = -1;
        //    for (var i = 0; i < Slots.Length; i++)
        //    {
        //        if (Slots[i].DbId == dbID && !Slots[i].IsFilled)
        //        {
        //            id = Slots[i].DbId;
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //public bool FreeSlotExistsIndex(out int freeSlotID)
        //{
        //    freeSlotID = Array.FindIndex(Slots, x => x.State == SlotState.Empty);
        //    return freeSlotID >= 0;
        //}

        //public void SetItemInSlot(int id, int dbId, int amount)
        //{
        //    if (Slots.Length <= id) return;
        //    Slots[id].DbId = dbId;
        //    Slots[id].IsEmpty = true;
        //    Slots[id].Amount += amount;
        //}
//
        //public void AddItemsToSlot(int id, int amount) => 
        //    Slots[id].Amount += amount;

        public void AddItemToInventory(int dbId, ItemPayloadData item,  int amount)
        {
            while (amount > 0)
            {
                var id = FindSlot(dbId);
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
                        amount = 0;
                        return;
                    }
                    else
                    {
                        Slots[id].State = SlotState.Full;
                        Slots[id].Amount += diff;
                        amount -= diff;
                    }
                }
                else
                    return; 
            }
        }

        private int FindSlot(int dbId)
        {
            for (var i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].DbId == dbId && Slots[i].State == SlotState.Middle)
                    return i;
            }

            return FindEmptySlot();
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

        public void RemoveItemFromSlot(int id)
        {
            if (Slots.Length <= id || Slots[id].State == SlotState.Empty) return;
            Slots[id].DbId = -1;
            Slots[id].State = SlotState.Empty;
            Slots[id].Amount = 0;
        }

        //public void ChangeCapacity(int amount)
        //{
        //    if (amount >= 0)
        //    { 
        //        var slots = Slots;
        //        Slots = new InventorySlot[amount];
        //        slots?.CopyTo(Slots, amount);
        //    }
        //}
//
        //public void ChangeData(int i, InventorySlot slot) => 
        //    Slots[i] = slot;
    }
}
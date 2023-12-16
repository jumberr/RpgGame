using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData;
using NUnit.Framework;

namespace _Project.CodeBase.Editor.Tests
{
    public class InventoryTest
    {
        private Inventory _inventory;
        private ItemPayloadInfo _item;

        [SetUp]
        public void SetUp()
        {
            _inventory = new Inventory(new InventoryData { InventorySize = 10, HotBarSize = 5 });
            _item = new ItemPayloadInfo();
            _item.SetMaxInContainer(5);
        }

        [Test]
        public void AddItemToInventory_AddsItemSuccessfully()
        {
            _inventory.AddItemToInventory(1, _item, 3);

            Assert.AreEqual(3, _inventory.Slots[0].CommonItemPart.Amount);
        }

        [Test]
        public void AddItemWithReturnAmount_ReturnsCorrectAmount()
        {
            var remaining = _inventory.AddItemWithReturnAmount(1, _item, _item.MaxInContainer * _inventory.Slots.Length + 2);

            Assert.AreEqual(2, remaining);
        }

        [Test]
        public void RemoveItemFromInventory_RemovesItemSuccessfully()
        {
            _inventory.AddItemToInventory(1, _item, 3);
            _inventory.RemoveItemFromInventory(1, 2);

            Assert.AreEqual(1, _inventory.Slots[0].CommonItemPart.Amount);
        }

        [Test]
        public void RemoveItemFromSlot_RemovesItemSuccessfully()
        {
            _inventory.AddItemToInventory(1, _item, 3);
            _inventory.RemoveItemFromSlot(0);

            Assert.AreEqual(2, _inventory.Slots[0].CommonItemPart.Amount);
        }

        [Test]
        public void SwapSlots_SwapsSlotsSuccessfully()
        {
            _inventory.AddItemToInventory(1, _item, 3);
            _inventory.AddItemToInventory(2, _item, 2);
            _inventory.SwapSlots(0, 1);

            Assert.AreEqual(2, _inventory.Slots[0].CommonItemPart.DbId);
            Assert.AreEqual(1, _inventory.Slots[1].CommonItemPart.DbId);
        }

        [Test]
        public void RemoveAllItemsFromSlot_RemovesAllItemsSuccessfully()
        {
            _inventory.AddItemToInventory(1, _item, 3);
            _inventory.RemoveAllItemsFromSlot(0);

            Assert.AreEqual(Inventory.ErrorIndex, _inventory.Slots[0].CommonItemPart.DbId);
        }

        [Test]
        public void FindSlot_FindsCorrectSlot()
        {
            _inventory.AddItemToInventory(1, _item, 3);
            var slot = _inventory.FindSlot(1, 3);

            Assert.AreEqual(3, slot.CommonItemPart.Amount);
        }

        [Test]
        public void FindSlotOrEmpty_FindsCorrectSlot()
        {
            _inventory.AddItemToInventory(1, _item, 3);
            var slotIndex = _inventory.FindSlotOrEmpty(1);

            Assert.AreEqual(0, slotIndex);
        }

        [Test]
        public void FindSlotReversed_FindsCorrectSlot()
        {
            _inventory.AddItemToInventory(1, _item, 3);
            var slotIndex = _inventory.FindSlotReversed(1);

            Assert.AreEqual(0, slotIndex);
        }

        [Test]
        public void FindEmptySlot_FindsCorrectSlot()
        {
            _inventory.AddItemToInventory(1, _item, 3);
            var slotIndex = _inventory.FindEmptySlot();

            Assert.AreEqual(1, slotIndex);
        }
    }
}
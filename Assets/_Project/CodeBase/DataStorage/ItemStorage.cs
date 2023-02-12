namespace _Project.CodeBase.Data
{
    public class ItemStorage
    {
        private ItemsData _items = new ItemsData();

        public void ApplyProgress(ItemsData data) => 
            _items = data;

        public ItemsData CollectData() =>
            _items;

        public void AddData(ItemData data) => 
            _items.AddData(data);
        
        public void RemoveData(ItemData data) => 
            _items.RemoveData(data);
    }
}
﻿using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Logic.Interaction;

namespace _Project.CodeBase.Data
{
    public class MapStorage : ISavedProgress
    {
        private readonly ItemStorage _itemStorage;
        private readonly InteractableSpawner _spawner;
        private MapData _mapData;

        private MapStorage(ItemStorage itemStorage, InteractableSpawner interactableSpawner)
        {
            _spawner = interactableSpawner;
            _itemStorage = itemStorage;
        }

        public void Initialize() => 
            InitializeItems();

        public void LoadProgress(PlayerProgress progress)
        {
            _mapData = progress.MapData;
            _itemStorage.ApplyProgress(_mapData.ItemsData);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            SaveSpawnedItems();
            
            _mapData.Apply(_itemStorage.CollectData());
            progress.MapData = _mapData;
        }

        private void InitializeItems() => 
            _spawner.InitializeOnStart(_mapData.ItemsData.Items);

        private void SaveSpawnedItems()
        {
            var itemsData = _spawner.SaveItems();
            _itemStorage.ApplyProgress(itemsData);
        }
    }
}
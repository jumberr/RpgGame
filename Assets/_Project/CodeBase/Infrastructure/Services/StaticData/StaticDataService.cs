using System;
using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.StaticData.ItemsDataBase;
using _Project.CodeBase.StaticData.UI;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assetProvider;
        
        private PlayerStaticData _playerStaticData;
        private ProjectSettings _projectSettings;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        private ItemsDataBase _itemsDataBase;

        public StaticDataService(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public async UniTask LoadMenuStaticData() => 
            _projectSettings = await _assetProvider.Load<ProjectSettings>(StaticDataPath.ProjectSettingsDataPath);

        public async UniTask LoadGameStaticData() => 
            _playerStaticData = await _assetProvider.Load<PlayerStaticData>(StaticDataPath.PlayerDataPath);

        public async UniTask LoadItemsDataBase() => 
            _itemsDataBase = await _assetProvider.Load<ItemsDataBase>(StaticDataPath.ItemsDataBase);

        public async UniTask LoadUIWindowConfig()
        {
            _windowConfigs = (await _assetProvider.Load<WindowStaticData>(StaticDataPath.WindowStaticData))
                .Configs
                .ToDictionary(x => x.WindowId, x => x);
        }

        public PlayerStaticData ForPlayer()
        {
            if (_playerStaticData != null)
                return _playerStaticData;
            throw new NullReferenceException();
        }

        public ProjectSettings ForProjectSettings()
        {
            if (_projectSettings != null)
                return _projectSettings;
            throw new NullReferenceException();
        }

        public ItemsDataBase ForInventory()
        {
            if (_itemsDataBase != null)
                return _itemsDataBase;
            throw new NullReferenceException();
        }

        public WindowConfig ForWindow(WindowId windowId) => 
            _windowConfigs[windowId];
    }
}
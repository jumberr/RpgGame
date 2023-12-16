using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.StaticData.Enemy;
using _Project.CodeBase.StaticData.UI;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assetProvider;

        private ProjectSettings _projectSettings;
        private ExceptionWindows _exceptionWindows;
        private PlayerStaticData _playerStaticData;
        private EnemyStaticData _enemyStaticData;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        private ItemsInfo _itemsInfo;

        public StaticDataService(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public async UniTask LoadProjectConfig() => 
            _projectSettings = await _assetProvider.Load<ProjectSettings>(StaticDataPath.ProjectSettingsDataPath);

        public async UniTask LoadGameStaticData()
        {
            _playerStaticData = await _assetProvider.Load<PlayerStaticData>(StaticDataPath.PlayerDataPath);
            _enemyStaticData = await _assetProvider.Load<EnemyStaticData>(StaticDataPath.EnemyDataPath);
        }

        public async UniTask LoadItemsDataBase() => 
            _itemsInfo = await _assetProvider.Load<ItemsInfo>(StaticDataPath.ItemsInfo);

        public async UniTask LoadUIWindowConfig() =>
            _windowConfigs = (await _assetProvider.Load<WindowStaticData>(StaticDataPath.WindowStaticData))
                .Configs
                .ToDictionary(x => x.WindowId, x => x);

        public async UniTask LoadExceptionWindows() => 
            _exceptionWindows = await _assetProvider.Load<ExceptionWindows>(StaticDataPath.ExceptionWindows);

        public ProjectSettings ForProjectSettings() => 
            _projectSettings;

        public ExceptionWindows ForWindowService() => 
            _exceptionWindows;

        public PlayerStaticData ForPlayer() => 
            _playerStaticData;

        public EnemyStaticData ForEnemy() => 
            _enemyStaticData;

        public ItemsInfo ForInventory() => 
            _itemsInfo;

        public WindowConfig ForWindow(WindowId windowId) => 
            _windowConfigs[windowId];
    }
}
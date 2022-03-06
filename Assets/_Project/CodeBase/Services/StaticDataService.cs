using System;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Services
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assetProvider;
        
        private PlayerStaticData _playerStaticData;
        private ProjectSettings _projectSettings;

        public StaticDataService(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public async UniTask LoadMenuStaticData()
        {
            _projectSettings = await _assetProvider.Load<ProjectSettings>(StaticDataPath.ProjectSettingsDataPath);
        }

        public async UniTask LoadGameStaticData()
        {
            _playerStaticData = await _assetProvider.Load<PlayerStaticData>(StaticDataPath.PlayerDataPath);
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
    }
}
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

        public StaticDataService(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public async UniTask Load() => 
            _playerStaticData = await _assetProvider.Load<PlayerStaticData>(StaticDataPath.PlayerStaticDataPath);

        public PlayerStaticData ForPlayer()
        {
            if (_playerStaticData != null)
                return _playerStaticData;
            throw new NullReferenceException();
        }
    }
}
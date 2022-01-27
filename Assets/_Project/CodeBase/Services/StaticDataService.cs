using System;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Services
{
    public class StaticDataService : IStaticDataService
    {
        private const string PlayerPath = "StaticData/PlayerStaticData";
        private PlayerStaticData _playerStaticData;

        public async UniTask Load() => 
            _playerStaticData = (PlayerStaticData) await Resources.LoadAsync<PlayerStaticData>(PlayerPath);

        public PlayerStaticData ForPlayer()
        {
            if (_playerStaticData != null)
                return _playerStaticData;
            throw new NullReferenceException();
        }
    }
}
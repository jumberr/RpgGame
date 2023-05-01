using System;
using _Project.CodeBase.Logic.Enemy;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic
{
    public class AIObserver
    {
        private const int MaxEnemies = 5;
        private const float DelayTime = 5f;

        private readonly EnemyProvider _enemyProvider;
        private readonly EnemyFacade.Factory _enemyFactory;

        public AIObserver(EnemyProvider enemyProvider, EnemyFacade.Factory enemyFactory)
        {
            _enemyProvider = enemyProvider;
            _enemyFactory = enemyFactory;
        }

        public void Initialize() => 
            Observe().Forget();

        private async UniTaskVoid Observe()
        {
            while (Application.isPlaying)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(DelayTime));
                if (_enemyProvider.Enemies.Count < MaxEnemies)
                    _enemyFactory.Create();
            }
        }
    }
}
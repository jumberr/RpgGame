using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.StaticData.Enemy;
using Zenject;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyFactory : IFactory<EnemyFacade>
    {
        private readonly DiContainer _container;
        private readonly EnemyProvider _provider;
        private readonly EnemyStaticData _enemyStaticData;

        public EnemyFactory(DiContainer container, EnemyProvider provider, IStaticDataService staticDataService)
        {
            _container = container;
            _provider = provider;
            _enemyStaticData = staticDataService.ForEnemy();
        }
        
        public virtual EnemyFacade Create()
        {
            var prefab = _enemyStaticData.EnemyPrefab;
            var enemy = _container.InstantiatePrefabForComponent<EnemyFacade>(prefab);
            _provider.Add(enemy);
            return enemy;
        }
    }
}
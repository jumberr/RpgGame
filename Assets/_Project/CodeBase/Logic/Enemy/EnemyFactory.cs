using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.StaticData.Enemy;
using Zenject;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyFactory : IFactory<EnemyFacade>
    {
        private readonly DiContainer _container;
        private readonly EnemyStaticData _enemyStaticData;

        public EnemyFactory(DiContainer container, IStaticDataService staticDataService)
        {
            _container = container;
            _enemyStaticData = staticDataService.ForEnemy();
        }
        
        public virtual EnemyFacade Create()
        {
            var prefab = _enemyStaticData.EnemyPrefab;
            return _container.InstantiatePrefabForComponent<EnemyFacade>(prefab);
        }
    }
}
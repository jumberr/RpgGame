using Zenject;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyObjectPool : MonoMemoryPool<EnemyFacade>
    {
        private readonly EnemyProvider _enemyProvider;

        public EnemyObjectPool(EnemyProvider provider) => 
            _enemyProvider = provider;

        protected override void Reinitialize(EnemyFacade enemy)
        {
            base.Reinitialize(enemy);
            _enemyProvider.Add(enemy);
            enemy.Reinitialize();
        }

        protected override void OnDespawned(EnemyFacade enemy)
        {
            base.OnDespawned(enemy);
            _enemyProvider.Remove(enemy);
        }
    }
}
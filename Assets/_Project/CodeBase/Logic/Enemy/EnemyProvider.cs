using System.Collections.Generic;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyProvider
    {
        private readonly List<EnemyFacade> _enemies = new List<EnemyFacade>();
        
        public List<EnemyFacade> Enemies => _enemies;

        public void Add(EnemyFacade enemy) => 
            _enemies.Add(enemy);

        public void Remove(EnemyFacade enemy)
        {
            if (_enemies.Contains(enemy)) 
                _enemies.Remove(enemy);
        }
    }
}
using _Project.CodeBase.Logic.Hero;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyFacade : MonoBehaviour
    {
        [SerializeField] private EnemyMovement movement;
        
        private HeroFacade _player;

        [Inject]
        public void Construct(HeroFacade.Factory heroFactory)
        {
            _player = heroFactory.Instance;
            movement.Initialize(_player.transform);
        }
        
        public class Factory : PlaceholderFactory<EnemyFacade>
        {
        }
    }
}
using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Enemy.FSM;
using _Project.CodeBase.UI;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyFacade : MonoBehaviour
    {
        [SerializeField] private AIAgent agent;
        [SerializeField] private EnemyDeath death;
        [SerializeField] private EnemyHealth health;
        [SerializeField] private EnemyHealthBar healthBar;
        [SerializeField] private EnemyAnimationController animatorController;
        [SerializeField] private EnemyRagdoll ragdoll;
        [SerializeField] private DissolveEffect dissolveEffect;
        
        private List<Vector3> _spawnPoints;

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        { 
            _spawnPoints = staticDataService.ForEnemy().SpawnPoints;
            death.SetHealthComponent(health);
            ragdoll.Setup(health);
        }

        public void Reinitialize()
        {
            UpdatePosition();
            animatorController.EnableAnimator();
            agent.EnterInitialState();
            health.Reinitialize();
            healthBar.Reinitialize();
            death.Reinitialize();
            dissolveEffect.DisableEffect();
       }

        private void UpdatePosition() => 
            transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Count - 1)];

        public class Factory : PlaceholderFactory<EnemyFacade>
        {
        }
    }
}
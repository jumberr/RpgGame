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

        [Inject]
        public void Construct()
        {
            death.SetHealthComponent(health);
            ragdoll.Setup(health);
        }

        public void Reinitialize()
        {
            transform.position = new Vector3(Random.Range(-20, 20), 2, Random.Range(-20, 20));
            agent.EnterInitialState();
            health.Reinitialize();
            healthBar.Reinitialize();
            death.Reinitialize();
            animatorController.EnableAnimator();
            dissolveEffect.DisableEffect();
       }

        public class Factory : PlaceholderFactory<EnemyFacade>
        {
        }
    }
}
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.UI;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic
{
    public class EnemyHealth : BaseHealthComponent
    {
        [SerializeField] private EnemyHealthBar healthBar;

        [Inject]
        public void Construct(IStaticDataService staticData)
        {
            HealthData = staticData.ForEnemy().HealthData;
            healthBar.Setup(this);
        }
    }
}
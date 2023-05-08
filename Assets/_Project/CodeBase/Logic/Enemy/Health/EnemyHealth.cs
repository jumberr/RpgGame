using _Project.CodeBase.Infrastructure.Services.StaticData;
using Zenject;

namespace _Project.CodeBase.Logic
{
    public class EnemyHealth : BaseHealthComponent
    {
        [Inject]
        public void Construct(IStaticDataService staticData) => 
            HealthData = staticData.ForEnemy().HealthData;
    }
}
using _Project.CodeBase.StaticData.Enemy;
using Zenject;

namespace _Project.CodeBase.Logic
{
    public class EnemyHealth : BaseHealthComponent
    {
        [Inject]
        public void Construct(EnemyStaticData staticData) => 
            HealthData = staticData.HealthData;
    }
}
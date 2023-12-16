using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroHealth : BaseHealthComponent, ISavedProgress
    {
        public void LoadProgress(PlayerProgress progress)
        {
            HealthData = progress.HealthData;
            InvokeHealthChanged();
        }

        public void UpdateProgress(PlayerProgress progress) => 
            progress.HealthData = HealthData;
    }
}
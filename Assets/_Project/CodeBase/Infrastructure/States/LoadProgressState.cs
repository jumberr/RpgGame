using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.SaveLoad;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Inventory;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticDataService;
        
        private readonly PositionData _defaultStartPosition = new PositionData(82, 4, 5);

        public LoadProgressState(
            IPersistentProgressService progressService,
            ISaveLoadService saveLoadService,
            IStaticDataService staticDataService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }

        public async UniTask Enter()
        {
            await LoadConfigs();
            await LoadProgressOrInitNew();
        }

        public void Exit() { }

        private async UniTask LoadConfigs()
        {
            await _staticDataService.LoadItemsDataBase();
            await _staticDataService.LoadUIWindowConfig();
            await _staticDataService.LoadExceptionWindows();
        }
        
        private async UniTask LoadProgressOrInitNew()
        {
            await _staticDataService.LoadGameStaticData();
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            var playerData = _staticDataService.ForPlayer();
            var inventory = new Inventory(8);
            var settings = new SettingsData(0.2f, 0);
            
            var healthData = new HealthData
            {
                CurrentHp = playerData.HealthData.CurrentHp,
                MaxHp = playerData.HealthData.MaxHp
            };

            var progress = new PlayerProgress(healthData, _defaultStartPosition, inventory, settings);
            return progress;
        }
    }
}
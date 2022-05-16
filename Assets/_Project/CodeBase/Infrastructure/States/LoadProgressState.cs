using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.SaveLoad;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Inventory;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly LazyInject<IGameStateMachine> _gameStateMachine;
        private readonly IStaticDataService _staticDataService;
        
        private readonly PositionData _defaultStartPosition = new PositionData(82, 4, 5);

        public LoadProgressState(IPersistentProgressService progressService,
            ISaveLoadService saveLoadService,
            LazyInject<IGameStateMachine> gameStateMachine,
            IStaticDataService staticDataService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
        }

        public async void Enter()
        {
            await LoadConfigs();
            await LoadProgressOrInitNew();
            _gameStateMachine.Value.Enter<InitializeGameSceneState>();
        }

        public void Exit() { }

        private async UniTask LoadConfigs()
        {
            await _staticDataService.LoadItemsDataBase();
            await _staticDataService.LoadUIWindowConfig();
        }
        
        private async UniTask LoadProgressOrInitNew()
        {
            await _staticDataService.LoadGameStaticData();
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            var playerData = _staticDataService.ForPlayer();
            var healthData = new HealthData
            {
                CurrentHp = playerData.HealthData.CurrentHp,
                MaxHp = playerData.HealthData.MaxHp
            };
            var inventory = new Inventory(4);

            var progress = new PlayerProgress(healthData, _defaultStartPosition, inventory);
            return progress;
        }
    }
}
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.StaticData;
using Zenject;

namespace _Project.CodeBase.Infrastructure.Scenes
{
    public class Bootstrap : IInitializable
    {
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IStaticDataService _staticDataService;
        private readonly InitialScene _initialScene;

        public Bootstrap(SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            IStaticDataService staticDataService,
            InitialScene initialScene)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _staticDataService = staticDataService;
            _initialScene = initialScene;
        }

        public async void Initialize()
        {
            _loadingCurtain.Show();

            await _staticDataService.LoadProjectConfig();
            await _sceneLoader.LoadSceneAsync(_staticDataService.ForProjectSettings().InitialScene);
            _initialScene.Initialize();
        }
    }
}
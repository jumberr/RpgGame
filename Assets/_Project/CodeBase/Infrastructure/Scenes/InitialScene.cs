using _Project.CodeBase.Infrastructure.Services.StaticData;

namespace _Project.CodeBase.Infrastructure
{
    public class InitialScene
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;

        public InitialScene(SceneLoader sceneLoader,
            IStaticDataService staticDataService)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
        }

        public async void Initialize()
        {
            var projectSettings = _staticDataService.ForProjectSettings();
            await _sceneLoader.LoadSceneAsync(projectSettings.MenuScene);
        }
    }
}
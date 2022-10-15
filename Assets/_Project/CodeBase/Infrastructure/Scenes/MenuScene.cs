using _Project.CodeBase.Infrastructure.Services.StaticData;
using Zenject;

namespace _Project.CodeBase.Infrastructure.Scenes
{
    public class MenuScene : IInitializable
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;

        public MenuScene(SceneLoader sceneLoader, IStaticDataService staticDataService)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
        }

        public async void Initialize()
        {
            var config = _staticDataService.ForProjectSettings();
            await _sceneLoader.LoadSceneAsync(config.GameScene);
        }
    }
}
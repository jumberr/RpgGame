using _Project.CodeBase.Infrastructure;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Effects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.CodeBase.UI.Windows.DeathScreen
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _respawnButton;
        private SceneLoader _sceneLoader;
        private IStaticDataService _staticDataService;
        
        public void Construct(SceneLoader sceneLoader, IStaticDataService staticDataService)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
        }

        private void Awake() => 
            _respawnButton.onClick.AddListener(Respawn);

        private void Start() => 
            Fade.DoFadeIn(_canvasGroup).Forget();

        private async void Respawn()
        {
            _respawnButton.enabled = false;
            //var config = _staticDataService.ForProjectSettings();
            await _sceneLoader.ReloadScene();
        }
    }
}
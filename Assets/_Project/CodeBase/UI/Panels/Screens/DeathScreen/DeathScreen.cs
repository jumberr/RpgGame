using _Project.CodeBase.Infrastructure;
using _Project.CodeBase.Logic.Effects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Screen = _Project.CodeBase.UI.Screens.Screen;

namespace _Project.CodeBase.UI
{
    public class DeathScreen : Screen
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _respawnButton;
        private SceneLoader _sceneLoader;
        
        [Inject]
        private void Construct(SceneLoader sceneLoader) => 
            _sceneLoader = sceneLoader;

        private void OnEnable() => 
            _respawnButton.onClick.AddListener(Respawn);

        private void Start() =>
            Hide();

        private void OnDisable() => 
            _respawnButton.onClick.RemoveListener(Respawn);

        public void Show() => 
            Fade.DoFadeIn(_canvasGroup).Forget();

        private void Hide() => 
            Fade.DoFadeOut(_canvasGroup).Forget();

        private async void Respawn()
        {
            _respawnButton.interactable = false;
            await _sceneLoader.ReloadScene();
        }
    }
}
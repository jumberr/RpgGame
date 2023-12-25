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

        private void Start()
        {
            ToggleRespawnButton(false);
            Hide();
        }

        private void OnDisable() => 
            _respawnButton.onClick.RemoveListener(Respawn);

        public void Show()
        {
            ToggleRespawnButton(true);
            Fade.DoFadeIn(_canvasGroup).Forget();
        }

        private void Hide() => 
            Fade.DoFadeOut(_canvasGroup).Forget();

        private async void Respawn()
        {
            ToggleRespawnButton(false);
            await _sceneLoader.ReloadScene();
        }

        private void ToggleRespawnButton(bool value) => 
            _respawnButton.interactable = value;
    }
}
using _Project.CodeBase.Infrastructure;
using _Project.CodeBase.Logic.Effects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _respawnButton;
        private SceneLoader _sceneLoader;
        
        public void Construct(SceneLoader sceneLoader) => 
            _sceneLoader = sceneLoader;

        private void OnEnable() => 
            _respawnButton.onClick.AddListener(Respawn);

        private void Start() => 
            Fade.DoFadeIn(_canvasGroup).Forget();

        private void OnDisable() => 
            _respawnButton.onClick.RemoveListener(Respawn);

        private async void Respawn()
        {
            _respawnButton.enabled = false;
            await _sceneLoader.ReloadScene();
        }
    }
}
using _Project.CodeBase.Infrastructure;
using _Project.CodeBase.Infrastructure.States;
using _Project.CodeBase.Logic.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows.DeathScreen
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _respawnButton;

        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        private void Awake() => 
            OnAwake();

        private void Start() => 
            OnStart();

        private void OnAwake() => 
            _respawnButton.onClick.AddListener(Respawn);

        private void OnStart() => 
            StartCoroutine(Fade.DoFadeIn(_canvasGroup));

        private void Respawn()
        {
            _gameStateMachine.Enter<ReloadSceneState>();
            _respawnButton.enabled = false;
        }
    }
}
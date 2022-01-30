using _Project.CodeBase.Infrastructure;
using _Project.CodeBase.Infrastructure.States;
using _Project.CodeBase.Logic;
using _Project.CodeBase.Logic.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows.DeathScreen
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button respawnButton;

        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        private void Awake() => 
            OnAwake();

        private void Start() => 
            OnStart();

        private void OnAwake() => 
            respawnButton.onClick.AddListener(Respawn);

        private void OnStart() => 
            StartCoroutine(Fade.DoFadeIn(canvasGroup));

        private void Respawn()
        {
            _gameStateMachine.Enter<ReloadSceneState>();
            respawnButton.enabled = false;
        }
    }
}
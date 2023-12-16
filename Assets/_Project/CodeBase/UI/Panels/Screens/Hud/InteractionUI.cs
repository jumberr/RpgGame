using System;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.UI.MVA;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI
{
    public class InteractionUI : MonoBehaviour, IHideable
    {
        [SerializeField] private Button _button;
        
        private InputService _inputService;
        private HeroInteraction _heroInteraction;
        private Action _onInteract;

        public void Construct(HeroInteraction heroInteraction, InputService inputService)
        {
            _inputService = inputService;
            _heroInteraction = heroInteraction;
            
            _heroInteraction.OnStartHover += OnStartHover;
            _heroInteraction.OnEndHover += EndHover;
        }

        private void Start() => 
            Hide();

        private void OnDisable()
        {
            _heroInteraction.OnStartHover -= OnStartHover;
            _heroInteraction.OnEndHover -= EndHover;
        }
        
        public void Show() => 
            _button.gameObject.SetActive(true);

        public void Hide() => 
            _button.gameObject.SetActive(false);
        
        private void OnStartHover(Action onInteract)
        {
            Show();
            
            BufferInteractAction(onInteract);
            _inputService.InteractAction.Event += Interact;
            _button.onClick.AddListener(Interact);
        }

        private void Interact() => 
            _onInteract?.Invoke();

        private void BufferInteractAction(Action onInteract) => 
            _onInteract = onInteract;

        private void EndHover()
        {
            _inputService.InteractAction.Event -= Interact;
            _button.onClick.RemoveListener(Interact);
            _onInteract = null;
            Hide();
        }
    }
}
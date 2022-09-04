using System;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.UI.MVA;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Elements.Hud
{
    public class InteractionUI : MonoBehaviour, IHideable
    {
        [SerializeField] private Button _button;
        private Interaction _interaction;
        private Action _onInteract;

        public void Construct(Interaction interaction)
        {
            _interaction = interaction;
            
            _interaction.OnStartHover += OnStartHover;
            _interaction.OnEndHover += EndHover;
        }

        private void Start() => 
            Hide();

        private void OnDisable()
        {
            _interaction.OnStartHover -= OnStartHover;
            _interaction.OnEndHover -= EndHover;
        }
        
        public void Show() => 
            _button.gameObject.SetActive(true);

        public void Hide() => 
            _button.gameObject.SetActive(false);
        
        private void OnStartHover(Action onInteract)
        {
            Show();
            BufferInteractAction(onInteract);
            _button.onClick.AddListener(Interact);
        }

        private void Interact() => 
            _onInteract?.Invoke();

        private void BufferInteractAction(Action onInteract) => 
            _onInteract = onInteract;

        private void EndHover()
        {
            _button.onClick.RemoveAllListeners();
            _onInteract = null;
            Hide();
        }
    }
}
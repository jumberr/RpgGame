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

        private void OnStartHover(Action onInteract)
        {
            Show();
            _button.onClick.AddListener(() => onInteract());
        }

        private void EndHover()
        {
            _button.onClick.RemoveAllListeners();
            Hide();
        }

        public void Show() => 
            _button.gameObject.SetActive(true);

        public void Hide() => 
            _button.gameObject.SetActive(false);
    }
}
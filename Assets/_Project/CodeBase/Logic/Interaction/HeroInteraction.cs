using System;
using NTC.Global.Cache;
using UnityEngine;

namespace _Project.CodeBase.Logic.Interaction
{
    public class HeroInteraction : NightCache, INightRun
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private float _range;
        
        private IInteractable _currentTarget;
        private readonly Vector3 _defaultPosition = new Vector3(0.5F, 0.5F, 0);
        private bool _hoverStarted;
        
        public Action<Action> OnStartHover;
        public event Action OnEndHover;

        public void Run() => 
            RaycastForInteractable();

        private void RaycastForInteractable()
        {
            var ray = _mainCamera.ViewportPointToRay(_defaultPosition);

            if (Physics.Raycast(ray, out var hit, _range))
            {
                var interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    if (hit.distance <= interactable.MaxRange)
                    {
                        if (interactable == _currentTarget || _hoverStarted) return;
                        _currentTarget?.OnEndHover();
                        StartHover(interactable);
                    }
                    else
                        EndHover();
                }
                else
                    EndHover();
            }
            else
                EndHover();
        }

        private void StartHover(IInteractable interactable)
        {
            _currentTarget = interactable;
            _currentTarget.OnStartHover();
            OnStartHover?.Invoke(_currentTarget.OnInteract);
            _hoverStarted = true;
        }

        private void EndHover()
        {
            if (_currentTarget is null) return;
            _currentTarget.OnEndHover();
            _currentTarget = null;
            OnEndHover?.Invoke();
            _hoverStarted = false;
        }
    }
}
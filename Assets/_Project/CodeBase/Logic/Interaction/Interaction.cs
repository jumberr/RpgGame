using System;
using UnityEngine;

namespace _Project.CodeBase.Logic.Interaction
{
    public class Interaction : MonoBehaviour
    {
        public Action<Action> OnStartHover;
        public event Action OnEndHover;

        [SerializeField] private Camera _mainCamera;
        [SerializeField] private float _range;
        private IInteractable _currentTarget;
 
        private void Update() => 
            RaycastForInteractable();

        private void RaycastForInteractable()
        {
            var ray = _mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));;

            if (Physics.Raycast(ray, out var hit, _range))
            {
                var interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    if (hit.distance <= interactable.MaxRange)
                    {
                        if (interactable == _currentTarget) return;
                        if (!(_currentTarget is null))
                        {
                            _currentTarget.OnEndHover();
                            StartHover(interactable);
                        }
                        else
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
            //Debug.Log("OnStartHover");
        }

        private void EndHover()
        {
            if (_currentTarget is null) return;
            _currentTarget.OnEndHover();
            _currentTarget = null;
            OnEndHover?.Invoke();
            //Debug.Log("OnEndHover");
        }
    }
}
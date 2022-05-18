using UnityEngine;

namespace _Project.CodeBase.Logic.Interaction
{
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        [SerializeField] private float _maxRange = 3f;
        public virtual float MaxRange => _maxRange;
        public virtual void OnStartHover()
        {
            
        }
        public virtual void OnInteract()
        {
            
        }
        public virtual void OnEndHover()
        {
            
        }
    }
}
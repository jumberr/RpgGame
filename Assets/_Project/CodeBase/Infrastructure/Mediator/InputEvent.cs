using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Mediator
{
    public class InputEvent : MediatorEvent
    {
        public Vector3 Position { get; set; }

        
        public InputEvent(Vector3 position)
        {
            Position = position;
        }
    }
}
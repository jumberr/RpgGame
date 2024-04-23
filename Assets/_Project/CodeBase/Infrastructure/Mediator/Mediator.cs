using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Mediator
{
    public class Mediator : MonoBehaviour, IMediator
    {
        [SerializeReference] private List<IMediatorSystem> _mediatorSystems;


        private void Awake()
        {
            foreach (var mediatorSystem in _mediatorSystems) 
                mediatorSystem.Init(this);
        }

        public void Invoke<T>(T data) where T : MediatorEvent
        {
            foreach (var mediatorSystem in _mediatorSystems)
            {
                mediatorSystem.OnRaiseEvent(data);
                if (data.Used) break;
            }
        }
    }
}
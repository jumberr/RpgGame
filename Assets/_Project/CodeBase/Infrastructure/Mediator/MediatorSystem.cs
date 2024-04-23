namespace _Project.CodeBase.Infrastructure.Mediator
{
    public abstract class MediatorSystem<T> : IMediatorSystem where T : MediatorEvent
    {
        private IMediator _mediator;
        

        public void Init(IMediator mediator) => 
            _mediator = mediator;

        public void OnRaiseEvent(MediatorEvent data)
        {
            if (data is T tData)
            {
                OnRaiseEvent(tData);
                tData.Use();
            }
        }

        protected abstract void OnRaiseEvent(T data);
    }
}
namespace _Project.CodeBase.Infrastructure.Mediator
{
    public interface IMediatorSystem
    {
        void Init(IMediator mediator);
        void OnRaiseEvent(MediatorEvent data);
    }
}
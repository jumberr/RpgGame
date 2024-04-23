namespace _Project.CodeBase.Infrastructure.Mediator
{
    public interface IMediator
    {
        void Invoke<T>(T data) where T : MediatorEvent;
    }
}
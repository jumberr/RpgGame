namespace _Project.CodeBase.Infrastructure.Mediator
{
    public class MediatorEvent
    {
        public bool Used { get; protected set; }


        public virtual void Use() => 
            Used = true;
    }
}
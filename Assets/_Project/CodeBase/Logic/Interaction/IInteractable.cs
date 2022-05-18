namespace _Project.CodeBase.Logic.Interaction
{
    public interface IInteractable
    {
        float MaxRange { get; }

        void OnStartHover();
        void OnInteract();
        void OnEndHover();
    }
}
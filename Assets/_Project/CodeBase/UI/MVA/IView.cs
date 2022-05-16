namespace _Project.CodeBase.UI.MVA
{
    public interface IView<T> : IHideable
    {
        void Show(T data);
    }
}
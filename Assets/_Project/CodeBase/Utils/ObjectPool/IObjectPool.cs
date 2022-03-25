namespace _Project.CodeBase.Utils.ObjectPool
{
    public interface IObjectPool<T>
    {
        T GetFromPool();
        void PushToPool();
    }
}
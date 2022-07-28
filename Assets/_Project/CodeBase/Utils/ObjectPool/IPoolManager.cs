using UnityEngine;

namespace _Project.CodeBase.Utils.ObjectPool
{
    public interface IPoolManager<T>
    {
        void Initialize(string objName);
        T SpawnObject(T prefab, Vector3 position, Quaternion rotation, int size);
        void ReleaseObject(T clone);
        void CleanUp();
        void OnSpawn(T obj, Vector3 position, Quaternion rotation);
        void OnRelease(T obj);
        void OnInstantiate(T obj);
    }
}
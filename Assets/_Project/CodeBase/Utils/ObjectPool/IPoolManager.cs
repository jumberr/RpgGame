using UnityEngine;

namespace _Project.CodeBase.Utils.ObjectPool
{
    public interface IPoolManager
    {
        void Initialize();
        GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation);
        void ReleaseObject(GameObject clone);
        void CleanUp();
    }
}
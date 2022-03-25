using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.CodeBase.Utils.ObjectPool
{
    public class PoolManager : IPoolManager
    {
        private readonly Dictionary<GameObject, IObjectPool<GameObject>> _pools = new Dictionary<GameObject, IObjectPool<GameObject>>();
        private readonly Dictionary<GameObject, IObjectPool<GameObject>> _instancedObjectsPools = new Dictionary<GameObject, IObjectPool<GameObject>>();
        private GameObject _root;

        public void Initialize() => 
            _root = new GameObject { name = "PoolManager" };

        public GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!_pools.ContainsKey(prefab)) 
                WarmPool(prefab, 1);

            var pool = _pools[prefab];

            var clone = pool.GetFromPool();
            clone.transform.SetPositionAndRotation(position, rotation);
            clone.SetActive(true);

            _instancedObjectsPools.Add(clone, pool);
            return clone;
        }

        public void ReleaseObject(GameObject clone)
        {
            clone.SetActive(false);

            if (_instancedObjectsPools.ContainsKey(clone))
            {
                _instancedObjectsPools[clone].PushToPool();
                _instancedObjectsPools.Remove(clone);
            }
            else
                Debug.LogWarning("No pool contains the object: " + clone.name);
        }

        public void CleanUp()
        {
            _pools.Clear();
            _instancedObjectsPools.Clear();
        }

        private void WarmPool(GameObject prefab, int size)
        {
            if (_pools.ContainsKey(prefab))
                throw new Exception("Pool for prefab " + prefab.name + " has already been created");

            var pool = new ObjectPool<GameObject>(() => InstantiatePrefab(prefab), size);
            _pools[prefab] = pool;
        }

        private GameObject InstantiatePrefab(GameObject prefab)
        {
            var gameObject = Object.Instantiate(prefab, _root.transform, true);
            gameObject.SetActive(false);
            return gameObject;
        }
    }
}
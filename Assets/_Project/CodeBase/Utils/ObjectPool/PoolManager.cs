using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.CodeBase.Utils.ObjectPool
{
    public class PoolManager<T> : IPoolManager<T> where T: Object
    {
        private readonly Dictionary<T, IObjectPool<T>> _pools = new Dictionary<T, IObjectPool<T>>();
        private readonly Dictionary<T, IObjectPool<T>> _instancedObjectsPools = new Dictionary<T, IObjectPool<T>>();
        private T _prefab;
        private GameObject _root;

        public void Initialize(string objName) => 
            _root = new GameObject { name = objName };

        public T SpawnObject(T prefab, Vector3 position, Quaternion rotation, int size = 5)
        {
            if (!_pools.ContainsKey(prefab)) 
                WarmPool(prefab, size);

            var pool = _pools[prefab];

            var clone = pool.GetFromPool();
            OnSpawn(clone, position, rotation);

            _instancedObjectsPools.Add(clone, pool);
            return clone;
        }

        public void ReleaseObject(T clone)
        {
            OnRelease(clone);

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

        public virtual void OnSpawn(T obj, Vector3 position, Quaternion rotation) { }
        public virtual void OnRelease(T obj) { }
        public virtual void OnInstantiate(T obj) { }
        
        private void WarmPool(T prefab, int size)
        {
            if (_pools.ContainsKey(prefab))
                throw new Exception("Pool for prefab " + prefab.name + " has already been created");

            _prefab = prefab;
            var pool = new ObjectPool<T>(InstantiatePrefab, size);
            _pools[prefab] = pool;
            _prefab = null;
        }

        private T InstantiatePrefab(T prefab)
        {
            var gameObject = Object.Instantiate(prefab, _root.transform, true);
            OnInstantiate(prefab);
            return gameObject;
        }

        private T InstantiatePrefab() => 
            InstantiatePrefab(_prefab);
    }
}
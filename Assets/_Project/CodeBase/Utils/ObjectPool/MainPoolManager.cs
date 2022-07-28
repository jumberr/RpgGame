using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.CodeBase.Utils.ObjectPool
{
    public class MainPoolManager
    {
        private readonly ComponentPoolManager _componentPool;
        private readonly GameObjectPoolManager _gameObjectPool;

        public MainPoolManager()
        {
            _componentPool = new ComponentPoolManager();
            _gameObjectPool = new GameObjectPoolManager();
        }

        public void Initialize()
        {
            _componentPool.Initialize("Component Pool Manager");
            _gameObjectPool.Initialize("GameObject Pool Manager");
        }

        public Object SpawnObject(Object prefab, Vector3 position, Quaternion rotation, int size = 5)
        {
            return prefab switch
            {
                GameObject gameObject => _gameObjectPool.SpawnObject(gameObject, position, rotation, size),
                Component component => _componentPool.SpawnObject(component, position, rotation, size),
                _ => throw new InvalidOperationException()
            };
        }

        public void ReleaseObject(Object clone)
        {
            switch (clone)
            {
                case GameObject gameObject:
                    _gameObjectPool.ReleaseObject(gameObject);
                    break;
                case Component component:
                    _componentPool.ReleaseObject(component);
                    break;
            }
        }

        public void CleanUp()
        {
            _componentPool.CleanUp();
            _gameObjectPool.CleanUp();
        }
    }
}
using System;
using System.Collections.Generic;

namespace _Project.CodeBase.Utils.ObjectPool
{
    public class ObjectPool<T> : IObjectPool<T>
    {
        private readonly Stack<T> _pool = new Stack<T>();
        private readonly Stack<T> _used = new Stack<T>();
        private readonly Func<T, T> _factoryFunc;
        private readonly T _prefab;
        private readonly int _defaultAmountObj;

        public ObjectPool(Func<T, T> factoryFunc, T prefab, int defaultAmountObj)
        {
            _factoryFunc = factoryFunc;
            _prefab = prefab;
            _defaultAmountObj = defaultAmountObj;
            WarmPool();
        }

        public T GetFromPool()
        {
            if (_pool.Count == 0) 
                InitializeElement();
            
            var obj = _pool.Pop();
            _used.Push(obj);
            
            return obj;
        }

        public void PushToPool()
        {
            if (_used.Count <= 0) return;
            var obj = _used.Pop();
            _pool.Push(obj);
        }

        private void WarmPool()
        {
            for (var i = 0; i < _defaultAmountObj; i++) 
                InitializeElement();
        }

        private T InitializeElement()
        {
            var obj = _factoryFunc(_prefab);
            _pool.Push(obj);
            return obj;
        }
    }
}
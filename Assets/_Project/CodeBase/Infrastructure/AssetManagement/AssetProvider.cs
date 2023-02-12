using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();

        public void Initialize() => 
            Addressables.InitializeAsync();

        public async UniTask<GameObject> InstantiateAsync(string address, Vector3 at = default, Transform parent = null, Quaternion rotation = default) =>
            Object.Instantiate(await Load<GameObject>(address), at, rotation, parent);

        public async UniTask<T> InstantiateComponentAsync<T>(string address, Vector3 at = default, Transform parent = null, Quaternion rotation = default) where T : Component => 
            Object.Instantiate(await LoadComponent<T>(address), at, rotation, parent);

        public async UniTask<T> Load<T>(AssetReferenceGameObject assetReference) where T : class
        {
            if (_completedCache.TryGetValue(assetReference.AssetGUID, out var completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
                Addressables.LoadAssetAsync<T>(assetReference),
                cacheKey: assetReference.AssetGUID);
        }

        public async UniTask<T> LoadComponent<T>(string address) where T : Component => 
            (await Load<GameObject>(address)).GetComponent<T>();

        public async UniTask<T> Load<T>(string address) where T : class
        {
            if (_completedCache.TryGetValue(address, out var completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
                Addressables.LoadAssetAsync<T>(address),
                cacheKey: address);
        }

        public void Cleanup()
        {
            foreach (var resourceHandles in _handles.Values)
            foreach (var handle in resourceHandles) 
                Addressables.Release(handle);
            
            _completedCache.Clear();
            _handles.Clear();
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_handles.TryGetValue(key, out var resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }

        private async UniTask<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle =>
                _completedCache[cacheKey] = completeHandle;

            AddHandle(cacheKey, handle);

            return await handle.Task;
        }
    }
}
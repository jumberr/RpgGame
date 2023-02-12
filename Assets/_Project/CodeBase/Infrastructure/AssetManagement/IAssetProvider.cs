using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        void Initialize();
        UniTask<GameObject> InstantiateAsync(string address, Vector3 at = default, Transform parent = null, Quaternion rotation = default);
        UniTask<T> InstantiateComponentAsync<T>(string address, Vector3 at = default, Transform parent = null, Quaternion rotation = default) where T : Component;
        UniTask<T> Load<T>(AssetReferenceGameObject assetReference) where T : class;
        UniTask<T> LoadComponent<T>(string address) where T : Component;
        UniTask<T> Load<T>(string address) where T : class;
        void Cleanup();
    }
}
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        void Initialize();
        UniTask<GameObject> InstantiateAsync(string address);
        UniTask<GameObject> InstantiateAsync(string address, Vector3 at);
        UniTask<GameObject> InstantiateAsync(string address, Transform at);
        UniTask<T> Load<T>(AssetReferenceGameObject assetReference) where T : class;
        UniTask<T> Load<T>(string address) where T : class;
        void CleanUp();
    }
}
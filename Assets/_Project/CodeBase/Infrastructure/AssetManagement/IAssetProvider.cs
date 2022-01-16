using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path, Transform parent);
        UniTask<GameObject> InstantiateAsync(string path);
        UniTask<GameObject> InstantiateAsync(string path, Vector3 at);
        UniTask<GameObject> InstantiateAsync(string path, Transform parent);
    }
}
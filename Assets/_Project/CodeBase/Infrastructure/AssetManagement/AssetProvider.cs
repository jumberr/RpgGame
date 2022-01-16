using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public GameObject Instantiate(string path, Transform parent)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, parent);
        }
        
        public async UniTask<GameObject> InstantiateAsync(string path)
        {
            var prefab = await Resources.LoadAsync<GameObject>(path);
            return Object.Instantiate(prefab) as GameObject;
        }

        public async UniTask<GameObject> InstantiateAsync(string path, Vector3 at)
        {
            var prefab = await Resources.LoadAsync<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity) as GameObject;
        }

        public async UniTask<GameObject> InstantiateAsync(string path, Transform parent)
        {
            var prefab = await Resources.LoadAsync<GameObject>(path);
            return Object.Instantiate(prefab, parent) as GameObject;
        }
    }
}
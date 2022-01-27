using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public async UniTask<GameObject> InstantiateAsync(string path) => 
            (GameObject) await Resources.LoadAsync<GameObject>(path);
    }
}
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        UniTask<GameObject> InstantiateAsync(string path);
    }
}
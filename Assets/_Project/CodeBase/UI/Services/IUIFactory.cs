using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public interface IUIFactory
    {
        UniTask CreateUIRoot();
        UniTask<GameObject> CreateHud();
    }
}
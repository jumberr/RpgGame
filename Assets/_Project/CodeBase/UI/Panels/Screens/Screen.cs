using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI.Screens
{
    public class Screen : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<string, Transform, UniTask<Screen>>
        {
        }
    }
}
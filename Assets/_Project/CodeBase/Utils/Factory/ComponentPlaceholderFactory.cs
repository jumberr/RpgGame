using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.CodeBase.Utils.Factory
{
    public class ComponentPlaceholderFactory<T> : PlaceholderFactory<string, UniTask<T>>
    {
        private T _instance;

        public async UniTask<T> WaitInstance()
        {
            while (_instance is null) 
                await UniTask.Yield();

            return _instance;
        }

        public override async UniTask<T> Create(string path)
        {
            _instance = await base.Create(path);
            return _instance;
        }
    }
}
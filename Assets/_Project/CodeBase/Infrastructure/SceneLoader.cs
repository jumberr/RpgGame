using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Project.CodeBase.Infrastructure
{
    public class SceneLoader
    {
        public async UniTask LoadSceneAsync(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                return;
            }

            await SceneManager.LoadSceneAsync(nextScene);
            onLoaded?.Invoke();
        }

        public async UniTask ReloadScene() => 
            await SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        public async UniTask UnloadSceneAsync(string name) => 
            await SceneManager.UnloadSceneAsync(name);

        public async UniTask UnloadCurrentSceneAsync() => 
            await UnloadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
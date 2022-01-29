using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Project.CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        
        public async UniTask UnloadSceneAsync(string name) => 
            await SceneManager.UnloadSceneAsync(name);
        
        public async UniTask UnloadCurrentSceneAsync() => 
            await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);

        public IEnumerator ReloadScene(Action onLoaded = null)
        {
            var waitNextScene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            
            while (!waitNextScene.isDone)
                yield return null;
            
            onLoaded?.Invoke();
        }

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            var waitNextScene = SceneManager.LoadSceneAsync(nextScene);
            
            while (!waitNextScene.isDone)
                yield return null;
            
            onLoaded?.Invoke();
        }
    }
}
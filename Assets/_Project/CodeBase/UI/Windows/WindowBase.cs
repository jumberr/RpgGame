using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.UI;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase
{
    public abstract class WindowBase : MonoBehaviour
    {
        public WindowId WindowId;
        public UiAnimation Animation;
        protected IPersistentProgressService ProgressService;
        protected PlayerProgress Progress => ProgressService.Progress;
        
        public void Construct(IPersistentProgressService progressService) => 
            ProgressService = progressService;

        private void Awake()
        {
            SubscribeUpdates();
            Initialize();
            Animation.PrepareAnimation(transform);
        }

        private void OnDestroy() => 
            Cleanup();

        public async UniTask Show()
        {
            gameObject.SetActive(true);
            await OnShowing();
        }
        
        public async UniTask Hide()
        {
            await OnHiding();
            gameObject.SetActive(false);
        }
        
        protected virtual void Initialize() { }
        protected virtual void OnConstructInitialized() { }

        protected virtual void SubscribeUpdates()
        {
        }

        protected virtual async UniTask OnShowing() => 
            await PlayAnimation(UiAnimation.EndValue);

        protected virtual async UniTask OnHiding() =>
            await PlayAnimation(UiAnimation.StartValue);

        protected virtual void Cleanup()
        {
        }

        private async UniTask PlayAnimation(float time)
        {
            Animation.DoAnimation(time);
            await UniTask.Delay(TimeSpan.FromSeconds(Animation.AnimationTime));
        }
    }
}
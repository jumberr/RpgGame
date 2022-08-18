using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.UI.Animation;
using _Project.CodeBase.UI.Windows.Inventory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public UiAnimation Animation;
        public Button CloseButton;
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

        public async void Show()
        {
            gameObject.SetActive(true);
            await OnShowing();
        }
        
        public async void Hide()
        {
            await OnHiding();
            gameObject.SetActive(false);
        }
        
        protected virtual void Initialize() { }
        protected virtual void OnConstructInitialized() { }

        protected virtual void SubscribeUpdates() =>
            CloseButton.onClick.AddListener(Hide);
        
        protected virtual async UniTask OnShowing() => 
            await PlayAnimation(UiAnimation.EndValue);

        protected virtual async UniTask OnHiding() =>
            await PlayAnimation(UiAnimation.StartValue);

        protected virtual void Cleanup() => 
            CloseButton.onClick.RemoveListener(Hide);
        
        private async UniTask PlayAnimation(float time)
        {
            Animation.DoAnimation(time);
            await UniTask.Delay(TimeSpan.FromSeconds(Animation.AnimationTime));
        }
    }
}
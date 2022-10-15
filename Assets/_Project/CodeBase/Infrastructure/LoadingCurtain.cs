using _Project.CodeBase.Logic.Effects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;

        public bool IsActive { get; private set; }

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Show()
        {
            if (IsActive) return;
            gameObject.SetActive(true);
            _curtain.alpha = 1;
            IsActive = true;
        }

        public async UniTask Hide()
        {
            if (!IsActive) return;
            await DoFadeOut();
            IsActive = false;
        }

        private async UniTask DoFadeOut()
        {
            await Fade.DoFadeOut(_curtain);
            gameObject.SetActive(false);
        }
    }
}
using System.Collections;
using _Project.CodeBase.Logic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;
        
        private void Awake()
        {
            Application.targetFrameRate = 500;
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1;
        }

        public void Hide() => 
             StartCoroutine(DoFadeOut());

        private IEnumerator DoFadeOut()
        {
            yield return StartCoroutine(Fade.DoFadeOut(_curtain));
            gameObject.SetActive(false);
        }
    }
}
using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.CodeBase.Logic.Effects
{
    public class Fade
    {
        private const float Min = 0f;
        private const float Max = 1f;
        private const float DelayTime = 0.5f;

        public static async UniTask DoFadeOut(CanvasGroup group)
        {
            group.DOFade(Min, DelayTime);
            await UniTask.Delay(TimeSpan.FromSeconds(DelayTime));
        }
        
        public static async UniTask DoFadeIn(CanvasGroup group)
        {
            group.DOFade(Max, DelayTime);
            await UniTask.Delay(TimeSpan.FromSeconds(DelayTime));
        }
    }
}
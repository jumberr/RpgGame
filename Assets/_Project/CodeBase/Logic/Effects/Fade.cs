using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.Effects
{
    public static class Fade
    {
        private const float Step = 0.03f;
        private const float DelayTime = 0.03f;

        public static async UniTask DoFadeOut(CanvasGroup group)
        {
            while (group.alpha > 0)
            {
                group.alpha -= Step;
                await UniTask.Delay(TimeSpan.FromSeconds(DelayTime));
            }
        }
        
        public static async UniTask DoFadeIn(CanvasGroup group)
        {
            while (group.alpha < 1)
            {
                group.alpha += Step;
                await UniTask.Delay(TimeSpan.FromSeconds(DelayTime));
            }
        }
    }
}
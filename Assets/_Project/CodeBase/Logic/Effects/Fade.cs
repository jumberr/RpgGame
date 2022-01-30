using System.Collections;
using UnityEngine;

namespace _Project.CodeBase.Logic.Effects
{
    public static class Fade
    {
        private const float Step = 0.03f;
        private const float DelayTime = 0.03f;

        public static IEnumerator DoFadeOut(CanvasGroup group)
        {
            while (group.alpha > 0)
            {
                group.alpha -= Step;
                yield return new WaitForSecondsRealtime(DelayTime);
            }
        }
        
        public static IEnumerator DoFadeIn(CanvasGroup group)
        {
            while (group.alpha < 1)
            {
                group.alpha += Step;
                yield return new WaitForSecondsRealtime(DelayTime);
            }
        }
    }
}
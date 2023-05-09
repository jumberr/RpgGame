using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic
{
    public class DissolveEffect : MonoBehaviour 
    {
        private const string DissolveAmount = "_DissolveAmount";
        private const int DisabledEffect = 0;
        private const int EnabledEffect = 1;
    
        [SerializeField] private Material material;

        private readonly int _dissolveAmount = Shader.PropertyToID(DissolveAmount);

        public async UniTaskVoid ActivateEffect()
        {
            var time = 0f;

            while (time < EnabledEffect)
            {
                material.SetFloat(_dissolveAmount, Mathf.Lerp(DisabledEffect, EnabledEffect, time));
                time += Time.deltaTime;
                await UniTask.Yield();
            }
        }

        public void DisableEffect() => 
            material.SetFloat(_dissolveAmount, DisabledEffect);
    }
}
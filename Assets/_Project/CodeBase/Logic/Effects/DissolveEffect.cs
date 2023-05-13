using Cysharp.Threading.Tasks;
using NTC.Global.Cache;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.CodeBase.Logic
{
    public class DissolveEffect : NightCache, INightInit 
    {
        private const string DissolveAmount = "_DissolveAmount";
        private const int DisabledEffect = 0;
    
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        [SerializeField] private int minDuration = 1;
        [SerializeField] private int maxDuration = 2;

        private readonly int _dissolveAmount = Shader.PropertyToID(DissolveAmount);
        private Material _material;
        
        public void Init() => 
            _material = skinnedMeshRenderer.material;

        public async UniTaskVoid ActivateEffect()
        {
            var duration = Random.Range(minDuration, maxDuration);
            var time = 0f;

            while (time < duration)
            {
                _material.SetFloat(_dissolveAmount, Mathf.Lerp(DisabledEffect, duration, time));
                time += Time.deltaTime;
                await UniTask.Yield();
            }
        }

        public void DisableEffect() => 
            _material.SetFloat(_dissolveAmount, DisabledEffect);
    }
}
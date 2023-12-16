using Cysharp.Threading.Tasks;
using NTC.Global.Cache;
using UnityEngine;

namespace _Project.CodeBase.Logic
{
    public class DissolveEffect : NightCache
    {
        private const string DissolveAmount = "_DissolveAmount";
        private const int DisabledEffect = 0;
    
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        [SerializeField] private int minDuration = 1;
        [SerializeField] private int maxDuration = 2;

        private readonly int _dissolveAmount = Shader.PropertyToID(DissolveAmount);
        private Material _material;

        private Material Material => _material ? _material : _material = skinnedMeshRenderer.material;

        public async UniTask ActivateEffect()
        {
            var duration = Random.Range(minDuration, maxDuration);
            var time = 0f;

            while (time < duration)
            {
                Material.SetFloat(_dissolveAmount, Mathf.Lerp(DisabledEffect, duration, time));
                time += Time.deltaTime;
                await UniTask.Yield();
            }
        }

        public void DisableEffect() => 
            Material.SetFloat(_dissolveAmount, DisabledEffect);
    }
}
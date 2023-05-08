using _Project.CodeBase.Logic;
using NTC.Global.Cache;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public class EnemyHealthBar : NightCache, INightInit, INightRun
    {
        private const string Fill = "_Fill";
        
        [SerializeField] private MeshRenderer meshRenderer;

        private readonly MaterialPropertyBlock _materialBlock = new MaterialPropertyBlock();
        private IHealth _health;
        private Transform _mainCamera;
        private static readonly int ShaderProperty = Shader.PropertyToID(Fill);

        public void Init() => 
            _mainCamera = Camera.main.transform;

        public void Run()
        {
            if (_mainCamera is null) return;
            
            AlignCamera();
        }

        private void OnDestroy() => 
            _health.HealthChanged -= UpdateHealth;

        public void Setup(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHealth;
        }

        private void UpdateHealth() 
        {
            meshRenderer.GetPropertyBlock(_materialBlock);
            _materialBlock.SetFloat(ShaderProperty, _health.Current / _health.Max);
            meshRenderer.SetPropertyBlock(_materialBlock);
        }
        
        private void AlignCamera()
        {
            var forward = transform.position - _mainCamera.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, _mainCamera.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }
}
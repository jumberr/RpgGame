using _Project.CodeBase.Logic;
using _Project.CodeBase.Utils.Extensions;
using NTC.Global.Cache;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public class EnemyHealthBar : NightCache, INightInit, INightRun
    {
        private const string Fill = "_Fill";
        
        [SerializeField] private MeshRenderer meshRenderer;

        private IHealth _health;
        private MaterialPropertyBlock _materialBlock;
        private Transform _mainCamera;
        private static readonly int ShaderProperty = Shader.PropertyToID(Fill);

        public void Init()
        {
            _materialBlock = new MaterialPropertyBlock();
            
            // TODO: refactor
            _mainCamera = Camera.main.transform;
        }

        public void Run() => 
            AlignCamera();

        private void OnDestroy() => 
            Cleanup();

        public void Setup(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHealth;
        }

        public void Deactivate()
        {
            meshRenderer.Deactivate();
            Cleanup();
        }

        private void Cleanup() => 
            _health.HealthChanged -= UpdateHealth;

        private void UpdateHealth() 
        {
            meshRenderer.GetPropertyBlock(_materialBlock);
            _materialBlock.SetFloat(ShaderProperty, _health.Current / _health.Max);
            meshRenderer.SetPropertyBlock(_materialBlock);
        }
        
        private void AlignCamera()
        {
            if (_mainCamera is null) return;
            
            var forward = transform.position - _mainCamera.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, _mainCamera.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }
}
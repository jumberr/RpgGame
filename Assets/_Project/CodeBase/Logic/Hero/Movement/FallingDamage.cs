using _Project.CodeBase.Infrastructure.Services.Game;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Hero
{
    public class FallingDamage : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private float _fallThreshold;
        [SerializeField] private float _maxFallHeight;
        [SerializeField] private float _minDamage;
        [SerializeField] private float _maxDamage;

        private CameraShakerService _shakerService;
        private float _highestPoint;


        [Inject]
        private void Construct(CameraShakerService shakerService) => 
            _shakerService = shakerService;

        public void ApplyFallDamage(float currentPos)
        {
            var fallHeight = _highestPoint - currentPos;
            if (!(fallHeight > _fallThreshold)) return;
            
            var fallHeightPercentage = Mathf.InverseLerp(_fallThreshold, _maxFallHeight, fallHeight);
            var damage = Mathf.Lerp(_minDamage, _maxDamage, fallHeightPercentage);

            _shakerService.Shake();
            _health.TakeDamage(damage).Forget();
        }

        public void StorePosition(float yPosition)
        {
            if (yPosition > _highestPoint) 
                _highestPoint = yPosition;
        }

        public void ResetHighestPoint() => 
            _highestPoint = 0f;
    }
}
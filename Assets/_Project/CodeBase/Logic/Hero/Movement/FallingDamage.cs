using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class FallingDamage : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private float _fallThreshold;
        [SerializeField] private float _maxFallHeight;
        [SerializeField] private float _minDamage;
        [SerializeField] private float _maxDamage;

        private float _highestPoint;
        
        
        public void ApplyFallDamage(float currentPos)
        {
            var fallHeight = _highestPoint - currentPos;
            if (!(fallHeight > _fallThreshold)) return;
            
            var fallHeightPercentage = Mathf.InverseLerp(_fallThreshold, _maxFallHeight, fallHeight);
            var damage = Mathf.Lerp(_minDamage, _maxDamage, fallHeightPercentage);
                
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
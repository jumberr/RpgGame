using System;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.StaticData.Sound;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    [RequireComponent(typeof(ThrowView), typeof(AudioSource))]
    public class GrenadeView : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Vector3 _explosionOffset;
        [SerializeField] private float _destroyTime;

        private readonly Collider[] _colliders = new Collider[20];
        private ExplosiveGrenadeData _explosiveGrenadeData;
        

        private void OnDrawGizmosSelected()
        {
            if (_explosiveGrenadeData is null) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + _explosionOffset, _explosiveGrenadeData.ExplosionRadius);
        }

        public async UniTask Throw(ExplosiveGrenadeInfo grenadeInfo)
        {
            _explosiveGrenadeData = grenadeInfo.ExplosiveGrenadeData;
            
            await UniTask.Delay(TimeSpan.FromSeconds(_explosiveGrenadeData.ExplosionDelay));

            Explode();
        }

        private void Explode()
        {
            var explosion = Instantiate(_explosiveGrenadeData.ExplosionPrefab, transform.position + _explosionOffset, Quaternion.identity);
            PlayExplosionSound(_explosiveGrenadeData.SoundData);
            
            Destroy(explosion, _destroyTime); // todo maybe move to pool
            Destroy(gameObject);
        }

        private void NearbyForceApply()
        {
            var overlappedAmount = Physics.OverlapSphereNonAlloc(transform.position, _explosiveGrenadeData.ExplosionRadius, _colliders);

            for (var i = 0; i < overlappedAmount; i++)
            {
                if (!_colliders[i].TryGetComponent<Rigidbody>(out var rb)) continue;

                rb.AddExplosionForce(_explosiveGrenadeData.ExplosionForce, transform.position, _explosiveGrenadeData.ExplosionRadius);
            }
            
            Array.Clear(_colliders, 0, _colliders.Length);
        }

        private void PlayExplosionSound(AudioData data)
        {
            _audioSource.clip = data.AudioClip;
            _audioSource.volume = data.Volume;
            _audioSource.PlayDelayed(data.Delay);
        }
    }
}
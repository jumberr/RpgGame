using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class GrenadeView : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private Vector3 _explosionOffset;
        [SerializeField] private ExplosionGrenadeData _explosionGrenadeData;
        [SerializeField] private float _destroyTime;

        private readonly Collider[] _colliders = new Collider[20];
        
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + _explosionOffset, _explosionGrenadeData.ExplosionRadius);
        }

        public async UniTask Throw()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_explosionGrenadeData.ExplosionDelay));

            var explosion = Instantiate(_explosionPrefab, transform.position + _explosionOffset, Quaternion.identity);
            Destroy(explosion, _destroyTime); // todo maybe move to pool

            //Destroy(gameObject);
        }

        private void NearbyForceApply()
        {
            var overlappedAmount = Physics.OverlapSphereNonAlloc(transform.position, _explosionGrenadeData.ExplosionRadius, _colliders);

            for (var i = 0; i < overlappedAmount; i++)
            {
                if (!_colliders[i].TryGetComponent<Rigidbody>(out var rb)) continue;

                rb.AddExplosionForce(_explosionGrenadeData.ExplosionForce, transform.position, _explosionGrenadeData.ExplosionRadius);
            }
            
            Array.Clear(_colliders, 0, _colliders.Length);
        }
    }
}
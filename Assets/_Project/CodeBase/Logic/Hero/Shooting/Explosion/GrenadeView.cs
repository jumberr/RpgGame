using System;
using System.Collections.Generic;
using _Project.CodeBase.Constants;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.Utils;
using Cysharp.Threading.Tasks;
using NTC.Global.Pool;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    [RequireComponent(typeof(AudioSource))]
    public class GrenadeView : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Vector3 _initialRotation;
        [SerializeField] private Vector3 _explosionOffset;

        private readonly Collider[] _colliders = new Collider[20];
        private ExplosiveGrenadeInfo _grenadeInfo;
        private ExplosiveGrenadeData _explosiveGrenadeData;

        private Vector3 Position => Rigidbody.transform.position;
        private Vector3 ExplosionEffectPosition => Position + _explosionOffset;
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public List<Transform> IKTargets { get; private set; }


        private void OnDrawGizmosSelected()
        {
            if (_explosiveGrenadeData is null) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Position, _explosiveGrenadeData.ExplosionRadius);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(TagConstants.Player)) return;
            
            _audioSource.SetupAndPlay(_grenadeInfo.ImpactSoundData);
        }

        public async UniTask Throw(ExplosiveGrenadeInfo grenadeInfo)
        {
            _grenadeInfo = grenadeInfo;
            _explosiveGrenadeData = grenadeInfo.ExplosiveGrenadeData;
            
            await UniTask.Delay(TimeSpan.FromSeconds(_explosiveGrenadeData.ExplosionDelay));

            NearbyForceApply();
            await Explode();
        }

        public void ResetTransform() => 
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(_initialRotation));

        private void NearbyForceApply()
        {
            var overlappedAmount = Physics.OverlapSphereNonAlloc(Position, _explosiveGrenadeData.ExplosionRadius, _colliders);

            for (var i = 0; i < overlappedAmount; i++)
            {
                if (!_colliders[i].TryGetComponent<Rigidbody>(out var rb)) continue;

                rb.AddExplosionForce(_explosiveGrenadeData.ExplosionForce, Position, _explosiveGrenadeData.ExplosionRadius);
            }
            
            Array.Clear(_colliders, 0, _colliders.Length);
        }

        private async UniTask Explode()
        {
            var vsfx = NightPool.Spawn(_explosiveGrenadeData.ExplosionPrefab, ExplosionEffectPosition, Quaternion.identity);
            await vsfx.Play(_explosiveGrenadeData.SoundData);
            NightPool.Despawn(vsfx);
        }
    }
}
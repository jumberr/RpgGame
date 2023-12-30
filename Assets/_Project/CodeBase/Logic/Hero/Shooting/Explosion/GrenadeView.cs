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

        private readonly Collider[] _hitColliders = new Collider[20];
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

        public void ResetTransform()
        {
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(_initialRotation));
            Rigidbody.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
        }
        
        public void TogglePhysics(bool value)
        {
            Rigidbody.useGravity = value;
            
            if (value) return;
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.angularVelocity = Vector3.zero;
        }

        private void NearbyForceApply()
        {
            var overlappedAmount = Physics.OverlapSphereNonAlloc(Position, _explosiveGrenadeData.ExplosionRadius, _hitColliders);

            for (var i = 0; i < overlappedAmount; i++)
            {
                if (_hitColliders[i].TryGetComponent<Rigidbody>(out var rb))
                    rb.AddExplosionForce(_explosiveGrenadeData.ExplosionForce, Position, _explosiveGrenadeData.ExplosionRadius);

                if (_hitColliders[i].TryGetComponent<IHealth>(out var health))
                    DealDamage(_explosiveGrenadeData.MaxDamage, i, health);
            }
            
            Array.Clear(_hitColliders, 0, _hitColliders.Length);
        }

        private void DealDamage(float maxDamage, int i, IHealth health)
        {
            var dmg = maxDamage - Vector3.Distance(Position, _hitColliders[i].transform.position) * (maxDamage / _explosiveGrenadeData.ExplosionRadius);
            health.TakeDamage(dmg);
        }

        private async UniTask Explode()
        {
            var vsfx = NightPool.Spawn(_explosiveGrenadeData.ExplosionPrefab, ExplosionEffectPosition, Quaternion.identity);
            await vsfx.Play(_explosiveGrenadeData.SoundData);
            NightPool.Despawn(vsfx);
        }
    }
}
using System;
using System.Collections.Generic;
using _Project.CodeBase.Constants;
using _Project.CodeBase.Logic.Enemy;
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
        [SerializeField] private LayerMask _grenadeInHandsLayer;
        [SerializeField] private LayerMask _releasedGrenadeLayer;
        
        private readonly Collider[] _hitColliders = new Collider[20];
        private ExplosiveGrenadeInfo _grenadeInfo;
        private GrenadeSpecs _grenadeSpecs;
        private ExplosiveGrenadeData _explosiveGrenadeData;

        private Vector3 Position => Rigidbody.transform.position;
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
            
            _audioSource.SetupAndPlay(_grenadeSpecs.ImpactSoundData);
        }

        public async UniTask Throw(ExplosiveGrenadeInfo grenadeInfo, Vector3 force)
        {
            _grenadeInfo = grenadeInfo;
            _grenadeSpecs = _grenadeInfo.GetSpecs();
            _explosiveGrenadeData = grenadeInfo.ExplosiveGrenadeData;
            
            Rigidbody.AddForce(force, ForceMode.VelocityChange);
            await UniTask.Delay(TimeSpan.FromSeconds(_explosiveGrenadeData.ExplosionDelay));

            NearbyForceApply();
            await Explode();
        }

        public void ResetTransform()
        {
            UpdateLayer(_grenadeInHandsLayer);
            ToggleGrenadeMesh(true);
            transform.localRotation = Quaternion.Euler(_initialRotation);
            Rigidbody.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
        }
        
        public void UnparentGrenade()
        {
            transform.SetParent(null);
            UpdateLayer(_releasedGrenadeLayer);
        }
        
        public void TogglePhysics(bool value)
        {
            Rigidbody.isKinematic = !value;
            Rigidbody.useGravity = value;
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

                var dmg = CalculateDamage(_explosiveGrenadeData.MaxDamage, i);
                if (_hitColliders[i].TryGetComponent<IHealth>(out var health))
                    health.TakeDamage(dmg);
                
                if (_hitColliders[i].TryGetComponent<IHitBox>(out var hitBox))
                    hitBox.Hit(dmg);
            }
            
            Array.Clear(_hitColliders, 0, _hitColliders.Length);
        }

        private async UniTask Explode()
        {
            ToggleGrenadeMesh(false);
            var vsfx = NightPool.Spawn(_explosiveGrenadeData.ExplosionPrefab, Position, Quaternion.identity);
            await vsfx.Play(_explosiveGrenadeData.SoundData);
            NightPool.Despawn(vsfx);
        }

        private void ToggleGrenadeMesh(bool value) => 
            Rigidbody.gameObject.SetActive(value);

        private void UpdateLayer(LayerMask layerMask)
        {
            var layer = LayerUtils.ConvertLayerMaskToInt(layerMask);
            LayerUtils.SetLayerRecursive(gameObject, layer);
        }

        private float CalculateDamage(float maxDamage, int i) => 
            maxDamage - Vector3.Distance(Position, _hitColliders[i].transform.position) * (maxDamage / _explosiveGrenadeData.ExplosionRadius);
    }
}
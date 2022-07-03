using _Project.CodeBase.StaticData.ItemsDataBase.Types;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class MeleeWeapon : MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;
        
        private readonly Collider[] _hits = new Collider[3];
        private LayerMask _layerMask;

        private float _damage;
        private float _radius;

        public void Construct(LayerMask mask, Knife knife)
        {
            _layerMask = mask;

            _damage = knife.Damage;
            _radius = knife.Radius;
        }

        [UsedImplicitly]
        public void OnAttack()
        {
            for (var i = 0; i < Hit(); i++)
            {
                if (_hits[i].transform.TryGetComponent<IHealth>(out var health)) 
                    health.TakeDamage(_damage);
            }
        }

        private int Hit() => 
            Physics.OverlapSphereNonAlloc(_startPoint.position, _radius, _hits, _layerMask);
    }
}
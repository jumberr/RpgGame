using _Project.CodeBase.Logic.Enemy;
using _Project.CodeBase.StaticData;
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

        public void Construct(LayerMask mask, KnifeInfo knifeInfo)
        {
            _layerMask = mask;

            _damage = knifeInfo.WeaponSpecs.Damage;
            _radius = knifeInfo.KnifeSpecs.Radius;
        }

        [UsedImplicitly]
        public void OnAttack()
        {
            for (var i = 0; i < Hit(); i++)
            {
                if (_hits[i].transform.TryGetComponent<IHitBox>(out var hitBox)) 
                    hitBox.Hit(_damage);
            }
        }

        private int Hit() => 
            Physics.OverlapSphereNonAlloc(_startPoint.position, _radius, _hits, _layerMask);
    }
}
using UnityEngine;

namespace _Project.CodeBase.Logic
{
    public abstract class BaseMeleeAttack : MonoBehaviour
    {
        [SerializeField] private Transform weapon;

        protected readonly Collider[] Hits = new Collider[3];
        protected float Damage;
        private LayerMask _layerMask;

        [field: SerializeField] public SphereCollider SphereCollider { get; private set; }

        public void Construct(LayerMask mask, float damage)
        {
            Damage = damage;
            _layerMask = mask;
        }

        protected int Hit()
        {
            var center = weapon.TransformPoint(SphereCollider.center);
            return Physics.OverlapSphereNonAlloc(center, SphereCollider.radius, Hits, _layerMask);
        }
    }
}
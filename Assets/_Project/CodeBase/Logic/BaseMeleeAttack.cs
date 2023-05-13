using Nomnom.RaycastVisualization;
using UnityEngine;
#if UNITY_EDITOR
using Physics = Nomnom.RaycastVisualization.VisualPhysics;

#else
using Physics = UnityEngine.Physics;
#endif

namespace _Project.CodeBase.Logic
{
    public abstract class BaseMeleeAttack : MonoBehaviour
    {
        [SerializeField] private Transform weapon;
        [SerializeField] private SphereCollider sphereCollider;

        protected readonly Collider[] Hits = new Collider[3];
        protected float Damage;

        private LayerMask _layerMask;

        public void Construct(LayerMask mask, float damage)
        {
            Damage = damage;
            _layerMask = mask;
        }

        protected int Hit()
        {
            using (VisualLifetime.Create(3f))
            {
                var center = weapon.TransformPoint(sphereCollider.center);
                return Physics.OverlapSphereNonAlloc(center, sphereCollider.radius, Hits, _layerMask);
            }
        }
    }
}
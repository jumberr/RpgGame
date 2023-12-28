using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class GrenadeIKController : MonoBehaviour
    {
        private const float Zero = 0f;
        private const float One = 1f;
        
        [SerializeField] private List<TwoBoneIKConstraint> _constraints;
        
        public Vector3 ThrowPositionForward => SpawnPosition.position + SpawnPosition.forward;
        [field: SerializeField] public Transform SpawnPosition { get; private set; }


        public void Enable(List<Transform> targets)
        {
            for (var i = 0; i < _constraints.Count; i++) 
                UpdateConstraint(_constraints[i], targets[i], One);
        }

        public void Disable()
        {
            foreach (var constraint in _constraints) 
                UpdateConstraint(constraint, null, Zero);
        }

        private void UpdateConstraint(TwoBoneIKConstraint constraint, Transform target, float weight)
        {
            constraint.data.target = target;
            constraint.weight = weight;
        }
    }
}
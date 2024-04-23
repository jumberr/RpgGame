using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class GrenadeIKController : MonoBehaviour
    {
        private const float Zero = 0f;
        private const float One = 1f;
        
        [SerializeField] private RigBuilder _rigBuilder;
        [SerializeField] private List<TwoBoneIKConstraint> _constraints;
        
        private List<Transform> _targets;
        
        [field: SerializeField] public Transform SpawnPosition { get; private set; }


        public void Enable(List<Transform> targets) => 
            UpdateConstraints(targets, One);

        public void Disable() => 
            UpdateConstraints(null, Zero);

        private void UpdateConstraints(IReadOnlyList<Transform> targets, float weight)
        {
            for (var i = 0; i < _constraints.Count; i++) 
                UpdateConstraint(_constraints[i], targets?[i], weight);

            _rigBuilder.Build();
        }
        
        private void UpdateConstraint(TwoBoneIKConstraint constraint, Transform target, float weight)
        {
            constraint.data.target = target;
            constraint.weight = weight;
        }
    }
}
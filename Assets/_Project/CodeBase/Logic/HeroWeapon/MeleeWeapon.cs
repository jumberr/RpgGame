using System.Collections.Generic;
using _Project.CodeBase.Logic.Enemy;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class MeleeWeapon : BaseMeleeAttack
    {
        private List<Transform> _dealtDamage = new List<Transform>();

        [UsedImplicitly]
        public void OnAttack()
        {
            _dealtDamage.Clear();
            
            for (var i = 0; i < Hit(); i++)
            {
                if (Hits[i].transform.TryGetComponent<IHitBox>(out var hitBox))
                {
                    var enemyRoot = Hits[i].transform.root;
                    if (_dealtDamage.Contains(enemyRoot)) continue;

                    _dealtDamage.Add(enemyRoot);
                    hitBox.Hit(Damage);
                }
            }
        }
    }
}
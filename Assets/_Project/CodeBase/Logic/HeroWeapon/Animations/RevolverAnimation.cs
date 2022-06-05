using System;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon.Animations
{
    public class RevolverAnimation : MonoBehaviour
    {
        public event Action<int> AmmoEnabled;
        public event Action<int> AmmoAdded;

        [UsedImplicitly]
        public void OnAmmoEnabled(int ammoIndex) => 
            AmmoEnabled?.Invoke(ammoIndex);

        [UsedImplicitly]
        public void OnAmmoAdded(int ammoIndex) => 
            AmmoAdded?.Invoke(ammoIndex);
    }
}
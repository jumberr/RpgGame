using JetBrains.Annotations;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon.Animations
{
    public class Spas12Animation : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        
        [UsedImplicitly]
        public void OnAmmoDisabled() => 
            _bullet.SetActive(false);
        
        [UsedImplicitly]
        public void OnAmmoEnabled() => 
            _bullet.SetActive(true);
    }
}
using System.Collections.Generic;
using _Project.CodeBase.Logic.HeroWeapon.Animations;
using _Project.CodeBase.Logic.HeroWeapon.Data;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class WeaponConfiguration : MonoBehaviour
    {
        public Transform FirePoint;
        public GameObject LightPoint;
        public GameObject AdsPoint;
        public Transform AttachmentsParent;
        public List<GameObject> RevolverAmmo;
        
        // Camera scope
        public SniperScopeRender ScopeRender;
        // hands animator
        public Animator HandsAnimator;
        // melee weapon
        public MeleeWeapon MeleeWeapon;
        // revolver anim
        public RevolverAnimation RevolverAnimation;
    }
}
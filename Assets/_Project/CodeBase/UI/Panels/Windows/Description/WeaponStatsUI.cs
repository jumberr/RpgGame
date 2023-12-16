using System.Collections.Generic;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.Utils.Extensions;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public class WeaponStatsUI : Turnable
    {
        [SerializeField] private SliderStatsContainer _damage;
        [SerializeField] private SliderStatsContainer _accuracy;
        [SerializeField] private SliderStatsContainer _aimAccuracy;
        [SerializeField] private SliderStatsContainer _range;
        [SerializeField] private SliderStatsContainer _fireRate;
        [SerializeField] private BaseStatsContainer _clipSize;

        private List<Component> _excludeList = new List<Component>();

        private void Awake() => 
            _excludeList = new List<Component> {_accuracy, _aimAccuracy, _clipSize};

        public void UpdateView(ItemInfo weapon)
        {
            switch (weapon)
            {
                case GunInfo ranged:
                    UpdateWeapon(ranged);
                    break;
                case KnifeInfo knife:
                    UpdateKnife(knife);
                    break;
            }
        }

        private void UpdateWeapon(GunInfo gunInfo)
        {
            _excludeList.ActivateComponents();
            _accuracy.UpdateView(gunInfo.GunSpecs.Accuracy, 1);
            _aimAccuracy.UpdateView(gunInfo.GunSpecs.AimAccuracy, 1);
            _clipSize.UpdateView(gunInfo.GunSpecs.MagazineInfo.BulletsMax);
            UpdateWeaponView(gunInfo);
        }

        private void UpdateKnife(KnifeInfo knifeInfo)
        {
            _excludeList.DeactivateComponents();
            UpdateWeaponView(knifeInfo);
        }

        private void UpdateWeaponView(WeaponInfo gun)
        {
            _damage.UpdateView(gun.WeaponSpecs.Damage, 100);
            _range.UpdateView(gun.WeaponSpecs.Range, 100);
            _fireRate.UpdateView(gun.WeaponSpecs.FireRate, 1000);
        }
    }
}
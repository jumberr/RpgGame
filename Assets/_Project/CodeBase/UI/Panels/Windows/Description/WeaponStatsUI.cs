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
            var specs = gunInfo.GetSpecs();
            _excludeList.ActivateComponents();
            _accuracy.UpdateView(specs.Accuracy, 1);
            _aimAccuracy.UpdateView(specs.AimAccuracy, 1);
            _clipSize.UpdateView(specs.MagazineInfo.BulletsMax);
            UpdateWeaponView(specs);
        }

        private void UpdateKnife(KnifeInfo knifeInfo)
        {
            _excludeList.DeactivateComponents();
            UpdateWeaponView(knifeInfo.GetSpecs());
        }

        private void UpdateWeaponView(KnifeSpecs specs)
        {
            _damage.UpdateView(specs.Damage, 100);
            _range.UpdateView(specs.Range, 100);
            _fireRate.UpdateView(specs.FireRate, 1000);
        }
    }
}
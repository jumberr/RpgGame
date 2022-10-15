using System.Collections.Generic;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.StaticData.ItemsDataBase;
using _Project.CodeBase.StaticData.ItemsDataBase.Types;
using _Project.CodeBase.Utils.Extensions;
using UnityEngine;

namespace _Project.CodeBase.UI.Windows.Inventory
{
    public class WeaponStatsUI : MonoBehaviour
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

        public void UpdateView(ItemData weapon)
        {
            switch (weapon)
            {
                case Weapon ranged:
                    UpdateWeapon(ranged);
                    break;
                case Knife knife:
                    UpdateKnife(knife);
                    break;
            }
        }

        private void UpdateWeapon(Weapon weapon)
        {
            _excludeList.ActivateComponents();
            _damage.UpdateView(weapon.WeaponData.Damage, 100);
            _accuracy.UpdateView(weapon.WeaponData.Accuracy, 1);
            _aimAccuracy.UpdateView(weapon.WeaponData.AimAccuracy, 1);
            _range.UpdateView(weapon.WeaponData.Range, 100);
            _fireRate.UpdateView(weapon.WeaponData.Range, 1000);
            _clipSize.UpdateView(weapon.Magazine.BulletsMax);
        }

        private void UpdateKnife(Knife weapon)
        {
            _excludeList.DeactivateComponents();
            _damage.UpdateView(weapon.Damage, 100);
            _range.UpdateView(weapon.Range, 100);
            _fireRate.UpdateView(weapon.Range, 1000);
        }
    }
}
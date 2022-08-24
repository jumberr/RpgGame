using _Project.CodeBase.Logic.HeroWeapon;
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

        public void UpdateView(Weapon weapon)
        {
            _damage.UpdateView(weapon.WeaponData.Damage, 100);
            _accuracy.UpdateView(weapon.WeaponData.Accuracy, 1);
            _aimAccuracy.UpdateView(weapon.WeaponData.AimAccuracy, 1);
            _range.UpdateView(weapon.WeaponData.Range, 100);
            _fireRate.UpdateView(weapon.WeaponData.Range, 1000);
            _clipSize.UpdateView(weapon.Magazine.BulletsMax);
        }
    }
}
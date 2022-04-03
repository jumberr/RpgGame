using System;
using _Project.CodeBase.Logic.Hero;
using TMPro;
using UnityEngine;

namespace _Project.CodeBase.UI.Elements
{
    public class AmmoUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _ammoLeft;
        [SerializeField] private TMP_Text _ammoAll;
        private HeroAmmo _ammo;

        public void Construct(HeroAmmo ammo)
        {
            _ammo = ammo;
            _ammo.OnUpdateAmmo += UpdateAmmoText;
            UpdateAmmoText(_ammo.BulletLeft, _ammo.BulletAll);
        }

        private void OnDisable() => 
            _ammo.OnUpdateAmmo -= UpdateAmmoText;

        private void UpdateAmmoText(int ammoLeft, int ammoAll)
        {
            _ammoLeft.text = ammoLeft.ToString();
            _ammoAll.text = ammoAll.ToString();
        }
    }
}
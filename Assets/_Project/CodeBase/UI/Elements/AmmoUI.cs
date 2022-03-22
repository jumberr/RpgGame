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
        private HeroAmmoController _ammoController;

        public void Construct(HeroAmmoController ammoController)
        {
            _ammoController = ammoController;
            _ammoController.OnUpdateAmmo += UpdateAmmoText;
            _ammoController.UpdateAmmoUI();
        }

        private void OnDisable() => 
            _ammoController.OnUpdateAmmo -= UpdateAmmoText;

        private void UpdateAmmoText(int ammoLeft, int ammoAll)
        {
            _ammoLeft.text = ammoLeft.ToString();
            _ammoAll.text = ammoAll.ToString();
        }
    }
}
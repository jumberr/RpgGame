using _Project.CodeBase.Logic.Hero;
using TMPro;
using UnityEngine;

namespace _Project.CodeBase.UI.Elements
{
    public class AmmoUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _ammoLeft;
        [SerializeField] private TMP_Text _ammoAll;
        
        public void Construct(HeroAmmoController ammoController)
        {
            ammoController.OnUpdateAmmo += UpdateAmmoText;
            ammoController.UpdateAmmoUI();
        }
        
        private void UpdateAmmoText(int ammoLeft, int ammoAll)
        {
            _ammoLeft.text = ammoLeft.ToString();
            _ammoAll.text = ammoAll.ToString();
        }
    }
}
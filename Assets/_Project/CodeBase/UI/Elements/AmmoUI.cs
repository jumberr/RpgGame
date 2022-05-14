using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Elements
{
    public class AmmoUI : MonoBehaviour, IHideable
    {
        [SerializeField] private TMP_Text _ammoLeft;
        [SerializeField] private TMP_Text _ammoAll;
        [SerializeField] private TMP_Text _slash;
        [SerializeField] private Image _image;
        private HeroAmmo _ammo;

        public void Construct(HeroAmmo ammo)
        {
            _ammo = ammo;
            _ammo.OnUpdateAmmo += UpdateAmmoText;
            _ammo.OnChangeWeapon += ChangeWeapon;
            Hide();
        }

        private void OnDisable()
        {
            _ammo.OnUpdateAmmo -= UpdateAmmoText;
            _ammo.OnChangeWeapon -= ChangeWeapon;
        }
        
        public void Show() => 
            ComponentUtils.SetActive(true, _ammoLeft, _ammoAll, _slash, _image);

        public void Hide() =>
            ComponentUtils.SetActive(false, _ammoLeft, _ammoAll, _slash, _image);

        private void UpdateAmmoText(int ammoLeft, int ammoAll)
        {
            _ammoLeft.text = ammoLeft.ToString();
            _ammoAll.text = ammoAll.ToString();
        }

        private void ChangeWeapon(int ammoLeft, int ammoAll, Sprite sprite)
        {
            Show();
            UpdateAmmoText(ammoLeft, ammoAll);
            _image.sprite = sprite;
        }
    }
}
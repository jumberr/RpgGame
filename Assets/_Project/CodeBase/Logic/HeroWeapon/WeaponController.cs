using System;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.Inventory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class WeaponController : MonoBehaviour
    {
        public event Action<bool> OnSwitch;

        [SerializeField] private HeroInventory _inventory;
        [SerializeField] private InputService _inputService;
        [SerializeField] private HeroState _state;
        [SerializeField] private HeroShooting _shooting;
        [SerializeField] private HeroScoping _scoping;
        [SerializeField] private HeroAmmo _ammo;
        [SerializeField] private HeroReload _reload;
        [SerializeField] private HeroRecoil _recoil;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private Transform _parent;

        private GameObject _currentWeapon;
        private Weapon _weapon;
        private int _slotID = -1;

        private void Start()
        {
            _shooting.Construct(_state, _inputService, _ammo, _reload, _recoil, _animator);
            _inventory.OnUpdate += OnInventoryUpdate;
        }

        private void OnDisable() => 
            _inventory.OnUpdate -= OnInventoryUpdate;

        public async UniTask CreateNewWeapon(GameObject prefab, Weapon weapon, int slotID)
        {
            await DestroyWeapon();

            _currentWeapon = Instantiate(prefab, _parent);
            _animator.Construct(_currentWeapon.GetComponent<Animator>());
            await _animator.ShowWeapon();
            OnSwitch?.Invoke(true);
            Construct(weapon);
            _slotID = slotID;
        }

        private void Construct(Weapon weapon)
        {
            _weapon = weapon;

            _ammo.Construct(weapon);
            _reload.Construct(weapon);
            _recoil.Construct(weapon);
            UpdateData(weapon);
        }

        private void UpdateData(Weapon weapon)
        {
            var config = GetComponentInChildren<WeaponConfiguration>();
            _shooting.UpdateConfig(weapon.WeaponData, config);
            _scoping.Construct(config);
        }

        private async void OnInventoryUpdate()
        {
            if (_slotID == -1 || _weapon is null) return;
            if (_inventory.FindFirstItemIndex(_weapon.ItemUIData.Name) == -1) 
                await DestroyWeapon();
        }

        private async UniTask DestroyWeapon()
        {
            if (_slotID == -1) return;
            _ammo.HideUI();
            await _animator.HideWeapon();
            _animator.SetEmptyAnimator();
            Destroy(_currentWeapon);
            _weapon = null;
            OnSwitch?.Invoke(false);
        }
    }
}
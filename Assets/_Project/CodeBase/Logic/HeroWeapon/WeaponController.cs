using System;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Hero.Reload;
using _Project.CodeBase.Logic.Hero.Shooting;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon.Animations;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData.ItemsDataBase.Types;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class WeaponController : MonoBehaviour
    {
        public event Action<bool> OnSwitch;

        [SerializeField] private HeroInventory _inventory;
        [SerializeField] private HeroState _state;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroScoping _scoping;
        [SerializeField] private HeroAmmo _ammo;
        [SerializeField] private HeroReload _reload;
        [SerializeField] private HeroRecoil _recoil;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private WeaponSway _weaponSway;
        [SerializeField] private Transform _parent;

        private InputService _inputService;
        private GameObject _currentWeapon;
        private Weapon _weapon;
        private Knife _knife;
        private int _slotID = -1;

        private void Start() => 
            _inventory.OnUpdate += OnInventoryUpdate;

        private void OnDisable() => 
            _inventory.OnUpdate -= OnInventoryUpdate;

        public void Setup(InputService inputService)
        {
            _inputService = inputService;
            _attack.Construct(_state, _inputService, _ammo, _reload, _recoil, _animator);
            _weaponSway.SetInputService(inputService);
        }

        public async UniTask CreateNewWeapon(GameObject prefab, Weapon weapon, int slotID)
        {
            await CreateGun(prefab, slotID);
            Construct(weapon);
        }
        
        public async UniTask CreateNewMeleeWeapon(GameObject prefab, Knife knife, int slotID)
        {
            await CreateGun(prefab, slotID);
            Construct(knife);
        }

        private void Construct(Weapon weapon)
        {
            _weapon = weapon;

            _ammo.Construct(weapon);
            _recoil.Construct(weapon);
            SetupConfig(weapon);
            SetupAttachments();
        }

        private void Construct(Knife knife)
        {
            _knife = knife;
            
            _ammo.Construct(knife);
            _recoil.Construct(knife);
            SetupKnife(knife);
        }

        private void SetupConfig(Weapon weapon)
        {
            var config = _currentWeapon.GetComponent<WeaponConfiguration>();
            _currentWeapon.TryGetComponent<RevolverAnimation>(out var rev);
            _attack.UpdateStatsAndConfig(weapon, config);
            _reload.Construct(weapon, config, rev);
            _scoping.Construct(weapon, config);
        }

        private void SetupKnife(Knife knife)
        {
            _currentWeapon.GetComponent<MeleeWeapon>().Construct(_attack.LayerMask, knife);
            _attack.ApplyKnife(knife);
            _reload.Construct(knife);
            _scoping.Construct(knife);
        }

        private void SetupAttachments()
        {
            // load
            if (_weapon.DefaultAttachments is null)
            {
                
            }
        }

        private async UniTask CreateGun(GameObject prefab, int slotID)
        {
            if (_slotID == slotID) return;
            
            _slotID = slotID;
            await DestroyWeapon();

            _currentWeapon = Instantiate(prefab, _parent);
            _animator.Construct(_currentWeapon.GetComponent<Animator>());
            await _animator.ShowWeaponOnInit();
            OnSwitch?.Invoke(true);
        }

        private async void OnInventoryUpdate()
        {
            if (_slotID == -1) return;
            
            if (_weapon != null)
                if (_inventory.FindFirstItemIndex(_weapon.ItemUIData.Name) == -1)
                    await DestroyWeapon();

            if (_knife != null)
                if (_inventory.FindFirstItemIndex(_knife.ItemUIData.Name) == -1)
                    await DestroyWeapon();
        }

        private async UniTask DestroyWeapon()
        {
            if (_slotID == -1) return;
            _ammo.HideUI();
            await _animator.HideWeapon();
            _animator.SetEmptyAnimator();
            _attack.SetWeaponTypeNone();
            Destroy(_currentWeapon);
            _weapon = null;
            _knife = null;
            OnSwitch?.Invoke(false);
        }
    }
}
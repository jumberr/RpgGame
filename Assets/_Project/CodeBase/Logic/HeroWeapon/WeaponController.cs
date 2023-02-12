using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Hero.Reload;
using _Project.CodeBase.Logic.Hero.Shooting;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon.Data;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class WeaponController : MonoBehaviour
    {
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

        private readonly AttachmentController _attachmentController = new AttachmentController();
        private readonly CurrentWeapon _currentWeapon = new CurrentWeapon();

        private InputService _inputService;
        private ItemsInfo _itemsInfo;
        private WeaponItem _weaponItem;

        public event Action<bool> OnSwitch;

        public void Construct(ItemsInfo itemsInfo) => 
            _itemsInfo = itemsInfo;

        private void OnEnable() => 
            _inventory.OnUpdate += OnInventoryUpdate;

        private void OnDisable() => 
            _inventory.OnUpdate -= OnInventoryUpdate;

        public void Setup(InputService inputService)
        {
            _inputService = inputService;
            _attack.Construct(_state, _inputService, _ammo, _reload, _recoil, _animator);
            _weaponSway.SetInputService(_inputService);
        }

        public async UniTask CreateNewWeapon(int slotID, CommonItemPart part, GunInfo gunInfo, GameObject prefab)
        {
            _weaponItem = part.Item as WeaponItem;
            await CreateGun(slotID, prefab);
            ConstructWeapon(gunInfo);
            
            _attachmentController.Construct(_currentWeapon, _weaponItem.WeaponData);
            SetupAttachments();
        }
        
        public async UniTask CreateNewMeleeWeapon(GameObject prefab, KnifeInfo knifeInfo, int slotID)
        {
            await CreateGun(slotID, prefab);
            ConstructWeapon(knifeInfo);
        }
        
        private async UniTask CreateGun(int slotID, GameObject prefab)
        {
            if (_currentWeapon.SlotID == slotID) return;

            await DestroyWeapon();

            _currentWeapon.Create(slotID, prefab, _parent);
            _animator.Construct(_currentWeapon.WeaponConfiguration.HandsAnimator);
            await _animator.ShowWeaponOnInit();
            OnSwitch?.Invoke(true);
        }

        private async UniTask DestroyWeapon()
        {
            if (_currentWeapon.SlotID == Inventory.Inventory.ErrorIndex) return; // todo: upd error index naming
            
            _ammo.HideUI();
            await AnimatorHideWeapon();
            _attack.SetWeaponTypeNone();
            DestroyWeaponAndAttachments();

            OnSwitch?.Invoke(false);
        }

        private void ConstructWeapon(GunInfo gunInfo)
        {
            _currentWeapon.GunInfo = gunInfo;

            _ammo.Construct(_weaponItem.MagazineData, gunInfo);
            _recoil.Construct(gunInfo);
            SetupConfig(gunInfo);
        }

        private void ConstructWeapon(KnifeInfo knifeInfo)
        {
            _currentWeapon.KnifeInfo = knifeInfo;
            
            _ammo.Construct(knifeInfo);
            _recoil.Construct(knifeInfo);
            SetupKnife(knifeInfo);
        }

        private void SetupConfig(GunInfo gunInfo)
        {
            var config = _currentWeapon.WeaponConfiguration;
            _attack.UpdateStatsAndConfig(gunInfo, config);
            _reload.Construct(gunInfo, config, config.RevolverAnimation);
            _scoping.Construct(gunInfo, config);
        }

        private void SetupKnife(KnifeInfo knifeInfo)
        {
            _currentWeapon.WeaponConfiguration.MeleeWeapon.Construct(_attack.LayerMask, knifeInfo);
            _attack.ApplyKnife(knifeInfo);
            _reload.Construct(knifeInfo);
            _scoping.Construct(knifeInfo);
        }

        private async UniTask AnimatorHideWeapon()
        {
            await _animator.HideWeapon();
            _animator.SetEmptyAnimator();
        }

        private async void OnInventoryUpdate()
        {
            if (_currentWeapon.SlotID == Inventory.Inventory.ErrorIndex) return; // todo: upd error index naming
            
            await CheckIfDestroy(_currentWeapon.GunInfo);
            await CheckIfDestroy(_currentWeapon.KnifeInfo);
        }

        private async UniTask CheckIfDestroy(WeaponInfo weaponInfo)
        {
            if (weaponInfo != null && _inventory.FindFirstItemIndex(weaponInfo.UIInfo.Name) == Inventory.Inventory.ErrorIndex) // todo: upd error index naming
                await DestroyWeapon();
        }

        private void SetupAttachments()
        {
            if (_currentWeapon.GunInfo != null)
            {
                if (!_weaponItem.WeaponData.Modified)
                    CreateDefaultAttachments();
                else
                    CreateSavedAttachments();
            }
        }

        private void CreateDefaultAttachments()
        {
            foreach (var itemName in _currentWeapon.GunInfo.DefaultAttachments) 
                CreateAttachment(itemName);
        }

        private void CreateSavedAttachments()
        {
            foreach (var attachment in _weaponItem.WeaponData.Attachments) 
                CreateAttachment(attachment.Value);
        }

        private void CreateAttachment(ItemName itemName) => 
            _attachmentController.CreateAttachment((AttachmentInfo) _itemsInfo.FindItem(itemName));

        private void DestroyWeaponAndAttachments()
        {
            _attachmentController.DestroyAllAttachments();
            _currentWeapon.Destroy();
        }
    }
}
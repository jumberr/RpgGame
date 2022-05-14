using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Hero.State;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class WeaponController : MonoBehaviour
    {
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

        public void Construct(Weapon weapon)
        {
            _weapon = weapon;
            
            _ammo.Construct(_weapon);
            _reload.Construct(_weapon);
            _recoil.Construct(_weapon);
            UpdateData(weapon);
        }

        private void Start() => 
            _shooting.Construct(_state, _inputService, _ammo, _reload, _recoil, _animator);

        public async UniTask CreateNewWeapon(GameObject prefab, Weapon weapon)
        {
            if (_currentWeapon != null)
            {
                await _animator.HideWeapon();
                Destroy(_currentWeapon);
            }

            _currentWeapon = Instantiate(prefab, _parent);
            _animator.Construct(_currentWeapon.GetComponent<Animator>());
            await _animator.ShowWeapon();
            Construct(weapon);
        }

        private void UpdateData(Weapon weapon)
        {
            var config = GetComponentInChildren<WeaponConfiguration>();
            _shooting.UpdateConfig(weapon.WeaponData, config);
            _scoping.Construct(config);
        }
    }
}
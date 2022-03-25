using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.StaticData;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroRecoil : MonoBehaviour
    {
        [SerializeField] private HeroState _state;

        private Vector3 _recoil;
        private Vector3 _aimRecoil;
        private float _snappiness;
        private float _returnSpeed;
        
        private Vector3 _currentRotation;
        private Vector3 _targetRotation;

        public void Construct(WeaponData weaponData)
        {
            _recoil = weaponData.Weapon.Recoil;
            _aimRecoil = weaponData.Weapon.AimRecoil;
            _snappiness = weaponData.Weapon.Snappiness;
            _returnSpeed = weaponData.Weapon.ReturnSpeed;
        }
        
        private void Update()
        {
            _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
            _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snappiness * Time.fixedDeltaTime);
            transform.localRotation = Quaternion.Euler(_currentRotation);
        }

        public void RecoilFire()
        {
            if (_state.CurrentState == EHeroState.Scoping)
                _targetRotation += TargetRotation(_aimRecoil);
            else
                _targetRotation += TargetRotation(_recoil);
        }

        private Vector3 TargetRotation(Vector3 recoil) => 
            new Vector3(recoil.x, Random.Range(-recoil.y, recoil.y), Random.Range(-recoil.z, recoil.z));
    }
}
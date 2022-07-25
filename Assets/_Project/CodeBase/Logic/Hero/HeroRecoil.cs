﻿using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.StaticData.ItemsDataBase.Types;
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

        public void Construct(Weapon weapon)
        {
            _recoil = weapon.WeaponData.Recoil;
            _aimRecoil = weapon.WeaponData.AimRecoil;
            _snappiness = weapon.WeaponData.Snappiness;
            _returnSpeed = weapon.WeaponData.ReturnSpeed;
        }
        
        public void Construct(Knife knife)
        {
            _recoil = Vector3.zero;
            _aimRecoil = Vector3.zero;
            _snappiness = 0;
            _returnSpeed = 0;
        }
        
        private void Update()
        {
            _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
            _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snappiness * Time.fixedDeltaTime);
            transform.localRotation = Quaternion.Euler(_currentRotation);
        }

        public void RecoilFire()
        {
            if (_state.CurrentPlayerState == State.PlayerState.Scoping)
                _targetRotation += TargetRotation(_aimRecoil);
            else
                _targetRotation += TargetRotation(_recoil);
        }

        private Vector3 TargetRotation(Vector3 recoil) => 
            new Vector3(recoil.x, Random.Range(-recoil.y, recoil.y), Random.Range(-recoil.z, recoil.z));
    }
}
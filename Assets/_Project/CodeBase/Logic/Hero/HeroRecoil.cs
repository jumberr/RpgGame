using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.StaticData;
using NTC.Global.Cache;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroRecoil : NightCache, INightRun
    {
        [SerializeField] private HeroState _state;

        private Vector3 _recoil;
        private Vector3 _aimRecoil;
        private float _snappiness;
        private float _returnSpeed;
        
        private Vector3 _currentRotation;
        private Vector3 _targetRotation;

        public void Construct(GunInfo gunInfo)
        {
            _recoil = gunInfo.GunSpecs.Recoil;
            _aimRecoil = gunInfo.GunSpecs.AimRecoil;
            _snappiness = gunInfo.GunSpecs.Snappiness;
            _returnSpeed = gunInfo.GunSpecs.ReturnSpeed;
        }
        
        public void Construct(KnifeInfo knifeInfo)
        {
            _recoil = Vector3.zero;
            _aimRecoil = Vector3.zero;
            _snappiness = 0;
            _returnSpeed = 0;
        }

        public void Run() => 
            ProceedRecoil();

        public void RecoilFire()
        {
            if (_state.Aiming)
                _targetRotation += TargetRotation(_aimRecoil);
            else
                _targetRotation += TargetRotation(_recoil);
        }

        private void ProceedRecoil()
        {
            _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
            _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snappiness * Time.fixedDeltaTime);
            transform.localRotation = Quaternion.Euler(_currentRotation);
        }

        private Vector3 TargetRotation(Vector3 recoil) => 
            new Vector3(recoil.x, Random.Range(-recoil.y, recoil.y), Random.Range(-recoil.z, recoil.z));
    }
}
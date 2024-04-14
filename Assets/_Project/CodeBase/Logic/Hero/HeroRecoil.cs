using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.StaticData;
using NTC.Global.Cache;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroRecoil : NightCache, INightRun
    {
        [SerializeField] private HeroState _state;
        
        private Vector3 _currentRotation;
        private Vector3 _targetRotation;
        private GunSpecs _gunSpecs;
        
        
        public void Setup(GunInfo gunInfo) => 
            _gunSpecs = gunInfo.GetSpecs();

        public void Setup(KnifeInfo knifeInfo) => 
            _gunSpecs = knifeInfo.GetDefaultGunSpecs();

        public void Run() => 
            ProceedRecoil();

        public void RecoilFire()
        {
            if (_state.Aiming)
                _targetRotation += TargetRotation(_gunSpecs.AimRecoil);
            else
                _targetRotation += TargetRotation(_gunSpecs.Recoil);
        }

        private void ProceedRecoil()
        {
            _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _gunSpecs.ReturnSpeed * Time.deltaTime);
            _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _gunSpecs.Snappiness * Time.fixedDeltaTime);
            transform.localRotation = Quaternion.Euler(_currentRotation);
        }

        private Vector3 TargetRotation(Vector3 recoil) => 
            new Vector3(recoil.x, Random.Range(-recoil.y, recoil.y), Random.Range(-recoil.z, recoil.z));
    }
}
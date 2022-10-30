using NTC.Global.Cache;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Cam
{
    public class CameraHeight : NightCache, INightRun
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _interpolationSpeed;
        
        private float _height;

        public void Run()
        {
            var heightTarget = _characterController.height * 0.9f;
            _height = Mathf.Lerp(_height, heightTarget, _interpolationSpeed * Time.deltaTime);
            CachedTransform.localPosition = Vector3.up * _height;
        }
    }
}
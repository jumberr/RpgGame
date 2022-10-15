using NTC.Global.Cache;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class ClipPrevention : NightCache, INightRun
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private float _distance;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _clippingLayer;
        [SerializeField] private Vector3 _newDirection;
        [SerializeField] private AnimationCurve _offsetCurve;
        
        public void Run() => 
            PreventClipping();

        private void PreventClipping()
        {
            var lerpPos = Physics.SphereCast(GetRayFromCamera(), _radius, out var hit, _distance, _clippingLayer)
                    ? CalculateOffset(hit)
                    : 0;

            Mathf.Clamp01(lerpPos);
            
            transform.localRotation = 
                Quaternion.Lerp(Quaternion.Euler(Vector3.zero), Quaternion.Euler(_newDirection), lerpPos);
        }

        private Ray GetRayFromCamera() => 
            _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        private float CalculateOffset(RaycastHit hit) => 
            _offsetCurve.Evaluate(hit.distance / _distance);
    }
}
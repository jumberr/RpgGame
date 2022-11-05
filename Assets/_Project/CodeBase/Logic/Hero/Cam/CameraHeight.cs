using _Project.CodeBase.Logic.Hero.State;
using DG.Tweening;
using NTC.Global.Cache;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Cam
{
    public class CameraHeight : NightCache
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private HeroState _heroState;
        [SerializeField] private float _height;
        [SerializeField] private float _time;

        private void OnEnable() => 
            _heroState.OnCrouchingChanged += ChangeCameraHeight;

        private void Start() => 
            ChangeCameraHeight();

        private void OnDisable() => 
            _heroState.OnCrouchingChanged -= ChangeCameraHeight;

        private void ChangeCameraHeight() => 
            CachedTransform.DOLocalMoveY(_characterController.height * _height, _time);
    }
}
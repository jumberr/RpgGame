using _Project.CodeBase.Infrastructure.Services.InputService;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroDeath : BaseDeathComponent
    {
        [SerializeField] private HeroMovement move;

        private InputService _inputService;

        [Inject]
        public void Construct(InputService inputService) => 
            _inputService = inputService;

        protected override void ProduceHeroDeath()
        {
            var duration = 0.3f;
            _inputService.BlockInput();

            ApplyDeathPositionToHero(move.CharacterController, duration);

            var cams = gameObject.GetComponentsInChildren<Camera>();
            RotateHeroCamera(cams[0], duration);
            SetEmptyCullingMaskToGunCamera(cams[1]);
        }

        private void ApplyDeathPositionToHero(CharacterController cc, float duration)
        {
            var pos = gameObject.transform.position;
            pos.y -= cc.height;
            gameObject.transform.DOLocalMove(pos, duration);
        }

        private void RotateHeroCamera(Camera heroCam, float duration) => 
            heroCam.transform.DOLocalRotate(Vector3.left * 90, duration);

        private void SetEmptyCullingMaskToGunCamera(Camera weaponCamera) => 
            weaponCamera.cullingMask = 0;
    }
}
using _Project.CodeBase.Logic.Hero;
using CameraShake;

namespace _Project.CodeBase.Infrastructure.Services.Game
{
    public class CameraShakerService
    {
        private readonly CameraShaker _cameraShaker;


        public CameraShakerService(CameraShaker cameraShaker) => 
            _cameraShaker = cameraShaker;

        public void Initialize(HeroFacade heroFacade) => 
            _cameraShaker.SetCameraTransform(heroFacade.CameraTransform);

        public void Shake() => 
            _cameraShaker.ShakePresets.ShortShake3D();
    }
}
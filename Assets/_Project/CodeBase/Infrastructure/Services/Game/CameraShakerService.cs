using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Logic.Hero;
using CameraShake;

namespace _Project.CodeBase.Infrastructure.Services.Game
{
    public class CameraShakerService
    {
        private readonly IGameFactory _gameFactory;
        private readonly CameraShaker _cameraShaker;


        public CameraShakerService(IGameFactory gameFactory, CameraShaker cameraShaker)
        {
            _gameFactory = gameFactory;
            _cameraShaker = cameraShaker;
            
            _gameFactory.HeroCreated += HeroCreated;
        }

        public void Shake() => 
            _cameraShaker.ShakePresets.ShortShake3D();
        
        private void HeroCreated(HeroFacade heroFacade)
        {
            _gameFactory.HeroCreated -= HeroCreated;
            _cameraShaker.SetCameraTransform(heroFacade.CameraTransform);
        }
    }
}
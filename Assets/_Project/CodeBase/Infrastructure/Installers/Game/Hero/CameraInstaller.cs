using _Project.CodeBase.Infrastructure.Services.Game;
using CameraShake;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private CameraShaker _shaker;
        
        
        public override void InstallBindings()
        {
            Container
                .Bind<CameraShakerService>()
                .AsSingle()
                .WithArguments(_shaker);
        }
    }
}
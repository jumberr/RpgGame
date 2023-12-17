using _Project.CodeBase.Infrastructure.Services;
using _Project.CodeBase.Infrastructure.Services.Game.Sound;
using _Project.CodeBase.StaticData.Sound;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class SoundInstaller : MonoInstaller
    {
        [SerializeField] private SurfaceSoundConfig _surfaceSoundConfig;
        [Space]
        [SerializeField] private AudioSourceWrapper _audioSourceWrapper;
        [SerializeField] private Transform _poolParent;
        
        
        public override void InstallBindings()
        {
            Container
                .BindMemoryPool<AudioSourceWrapper, AudioSourceWrapper.Pool>()
                .FromComponentInNewPrefab(_audioSourceWrapper)
                .UnderTransform(_poolParent);
            
            Container
                .Bind<AudioSourcePool>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<SoundService>()
                .AsSingle()
                .WithArguments(_surfaceSoundConfig);
        }
    }
}
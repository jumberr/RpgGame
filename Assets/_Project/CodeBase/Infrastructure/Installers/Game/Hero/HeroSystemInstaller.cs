using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Hero.Reload;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class HeroSystemInstaller : MonoInstaller
    {
        [SerializeField] private ManeuverStateMachine _maneuverStateMachine;
        
        
        public override void InstallBindings()
        {
            Container
                .Bind<VelocityController>()
                .AsSingle();
            
            BindManeuverStateMachine();

            BindReloadSystem<DefaultReloadSystem>();
            BindReloadSystem<RevolverReloadSystem>();
        }

        private void BindManeuverStateMachine()
        {
            BindManeuverState<MoveState>();
            BindManeuverState<JumpState>();
            BindManeuverState<CrouchState>();
            
            Container
                .BindInstance(_maneuverStateMachine)
                .AsSingle();
        }

        private void BindManeuverState<T>() where T : ManeuverState =>
            Container
                .Bind<ManeuverState>()
                .To<T>()
                .AsSingle();
        
        private void BindReloadSystem<T>() where T : IReloadSystem =>
            Container
                .BindInterfacesTo<T>()
                .AsSingle();
    }
}
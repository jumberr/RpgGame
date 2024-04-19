using _Project.CodeBase.Logic.Hero.Reload;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class HeroSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindReloadSystem<DefaultReloadSystem>();
            BindReloadSystem<RevolverReloadSystem>();
        }
        
        private void BindReloadSystem<T>() where T : IReloadSystem =>
            Container
                .BindInterfacesTo<T>()
                .AsSingle();
    }
}
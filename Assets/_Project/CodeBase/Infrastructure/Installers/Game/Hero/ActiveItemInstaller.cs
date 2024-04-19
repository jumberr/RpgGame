using _Project.CodeBase.Logic.HeroWeapon;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class ActiveItemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<AttachmentController>()
                .AsSingle();
            
            BindItemHandlers();
            BindWeaponBuilders();
        }

        private void BindItemHandlers()
        {
            BindItemHandler<WeaponHandler>();
            BindItemHandler<FoodItemHandler>();
        }
        
        private void BindWeaponBuilders()
        {
            BindWeaponBuilder<MeleeWeaponBuilder>();
            BindWeaponBuilder<RangeWeaponBuilder>();
            BindWeaponBuilder<RevolverWeaponBuilder>();
            BindWeaponBuilder<GrenadeWeaponBuilder>();
        }
        
        private void BindItemHandler<T>() where T : IItemHandler =>
            Container
                .Bind<IItemHandler>()
                .To<T>()
                .AsSingle();

        private void BindWeaponBuilder<T>() where T : IWeaponBuilder =>
            Container
                .Bind<IWeaponBuilder>()
                .To<T>()
                .AsSingle();
    }
}
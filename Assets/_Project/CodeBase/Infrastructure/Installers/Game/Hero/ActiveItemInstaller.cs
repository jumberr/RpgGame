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
            
            Container
                .Bind<IItemHandler>()
                .To<WeaponHandler>()
                .AsSingle();
            
            Container
                .Bind<IItemHandler>()
                .To<FoodItemHandler>()
                .AsSingle();
            
            BindWeaponBuilders();
        }

        private void BindWeaponBuilders()
        {
            Container
                .Bind<IWeaponBuilder>()
                .To<MeleeWeaponHandler>()
                .AsSingle();

            Container
                .Bind<IWeaponBuilder>()
                .To<RangeWeaponHandler>()
                .AsSingle();
        }
    }
}
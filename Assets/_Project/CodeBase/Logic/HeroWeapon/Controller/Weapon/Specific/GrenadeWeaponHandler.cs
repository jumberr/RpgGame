using _Project.CodeBase.Logic.Hero.Shooting;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class GrenadeWeaponHandler : RangeWeaponHandler
    {
        public override WeaponType Type => WeaponType.Grenade;


        protected GrenadeWeaponHandler(AttachmentController attachmentController) : base(attachmentController)
        {
        }
    }
}
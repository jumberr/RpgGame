using _Project.CodeBase.Logic.Hero.Shooting;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class RevolverWeaponHandler : RangeWeaponHandler
    {
        public override WeaponType Type => WeaponType.Revolver;
        

        protected RevolverWeaponHandler(AttachmentController attachmentController) : base(attachmentController)
        {
        }
    }
}
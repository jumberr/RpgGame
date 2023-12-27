using _Project.CodeBase.Logic.Hero.Shooting;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class ShotgunWeaponHandler : RangeWeaponHandler
    {
        public override WeaponType Type => WeaponType.Shotgun;

        
        public ShotgunWeaponHandler(AttachmentController attachmentController) : base(attachmentController)
        {
        }
    }
}
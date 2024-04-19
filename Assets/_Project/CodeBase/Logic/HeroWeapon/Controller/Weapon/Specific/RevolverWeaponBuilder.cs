using System;
using _Project.CodeBase.StaticData;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class RevolverWeaponBuilder : RangeWeaponBuilder
    {
        public override Type SupportedType => typeof(WeaponInfo<RevolverSpecs>);
        

        protected RevolverWeaponBuilder(AttachmentController attachmentController) : base(attachmentController)
        {
        }
    }
}
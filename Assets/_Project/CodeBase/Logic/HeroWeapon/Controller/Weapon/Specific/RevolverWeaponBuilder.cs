using System;
using _Project.CodeBase.StaticData;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class RevolverWeaponBuilder : RangeWeaponBuilder
    {
        public override Type[] SupportedTypes { get; } = {typeof(RevolverInfo)};
        

        protected RevolverWeaponBuilder(AttachmentController attachmentController) : base(attachmentController)
        {
        }
    }
}
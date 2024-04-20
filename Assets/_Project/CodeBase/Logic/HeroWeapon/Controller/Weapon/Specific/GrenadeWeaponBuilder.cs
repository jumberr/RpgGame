using System;
using _Project.CodeBase.StaticData;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class GrenadeWeaponBuilder : RangeWeaponBuilder
    {
        public override Type SupportedType => typeof(GrenadeInfo);


        protected GrenadeWeaponBuilder(AttachmentController attachmentController) : base(attachmentController)
        {
        }
    }
}
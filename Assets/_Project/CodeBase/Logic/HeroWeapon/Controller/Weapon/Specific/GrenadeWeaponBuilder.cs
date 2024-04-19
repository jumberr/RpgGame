using System;
using _Project.CodeBase.StaticData;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class GrenadeWeaponBuilder : RangeWeaponBuilder
    {
        public override Type SupportedType => typeof(GrenadeInfo); // todo move to WeaponInfo<GrenadeSpecs>


        protected GrenadeWeaponBuilder(AttachmentController attachmentController) : base(attachmentController)
        {
        }
    }
}
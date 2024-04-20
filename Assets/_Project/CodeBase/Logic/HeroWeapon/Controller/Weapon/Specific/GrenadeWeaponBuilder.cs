using System;
using _Project.CodeBase.StaticData;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class GrenadeWeaponBuilder : RangeWeaponBuilder
    {
        public override Type[] SupportedTypes { get; } = {typeof(GrenadeInfo), typeof(ExplosiveGrenadeInfo)};


        protected GrenadeWeaponBuilder(AttachmentController attachmentController) : base(attachmentController)
        {
        }
    }
}
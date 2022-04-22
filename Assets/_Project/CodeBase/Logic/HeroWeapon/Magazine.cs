using System;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    [Serializable]
    public class Magazine
    {
        public int BulletsMax;
        [HideInInspector] public int BulletsLeft;
    }
}
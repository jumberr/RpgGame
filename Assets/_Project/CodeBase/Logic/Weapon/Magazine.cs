using System;
using UnityEngine;

namespace _Project.CodeBase.Logic.Weapon
{
    [Serializable]
    public class Magazine
    {
        public int BulletsMax;
        [HideInInspector] public int BulletsLeft;
    }
}
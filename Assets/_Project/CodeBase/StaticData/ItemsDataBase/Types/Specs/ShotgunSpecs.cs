using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class ShotgunSpecs
    {
        [SerializeField] private int _fractionAmount;
        public int FractionAmount => _fractionAmount;
    }
}
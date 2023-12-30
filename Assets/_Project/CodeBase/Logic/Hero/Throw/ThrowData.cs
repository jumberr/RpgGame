using System;
using _Project.CodeBase.StaticData.Sound;

namespace _Project.CodeBase.Logic.Hero
{
    [Serializable]
    public class ThrowData
    {
        public float ThrowForce;
        public float MaxForce;
        public AudioData ThrowSoundData;
    }
}
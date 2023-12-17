using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData.Sound
{
    [Serializable]
    public class AudioData
    {
        public AudioClip AudioClip;
        public float Volume = 1f;
        public float Delay;
    }
}
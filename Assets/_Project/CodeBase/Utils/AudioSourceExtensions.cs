using _Project.CodeBase.StaticData.Sound;
using UnityEngine;

namespace _Project.CodeBase.Utils
{
    public static class AudioSourceExtensions
    {
        public static void SetupAndPlay(this AudioSource audioSource, AudioData data)
        {
            audioSource.clip = data.AudioClip;
            audioSource.volume = data.Volume;
            audioSource.PlayDelayed(data.Delay);
        }
    }
}
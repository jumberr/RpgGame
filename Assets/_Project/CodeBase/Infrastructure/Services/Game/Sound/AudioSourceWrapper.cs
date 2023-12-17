using _Project.CodeBase.StaticData.Sound;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure.Services
{
    public class AudioSourceWrapper : MonoBehaviour
    {
        private AudioData _data;
        [field: SerializeField] public AudioSource AudioSource { get; private set; }


        public AudioSourceWrapper Setup(AudioData data)
        {
            _data = data;
            AudioSource.clip = data.AudioClip;
            AudioSource.volume = data.Volume;
            
            UpdateName(data.AudioClip.name);
            return this;
        }

        private void UpdateName(string clipName)
        {
            gameObject.name = clipName;
        }

        public class Pool : MonoMemoryPool<AudioSourceWrapper>
        {
        }
    }
}
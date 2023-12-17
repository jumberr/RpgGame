using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.StaticData.Sound;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.Game.Sound
{
    public class AudioSourcePool
    {
        private readonly AudioSourceWrapper.Pool _audioSourcePool;
        private readonly List<AudioSourceWrapper> _audioSourcesWrappers = new List<AudioSourceWrapper>();

        
        public AudioSourcePool(AudioSourceWrapper.Pool pool) => 
            _audioSourcePool = pool;

        public AudioSourceWrapper GetAudioSourceWrapper(AudioData data)
        {
            foreach (var wrapper in _audioSourcesWrappers.Where(wrapper => !wrapper.AudioSource.isPlaying))
                return wrapper.Setup(data);

            return AddToPool().Setup(data);
        }

        public void ReturnToPool(AudioSourceWrapper wrapper)
        {
            wrapper.AudioSource.Stop();
            _audioSourcePool.Despawn(wrapper);
            _audioSourcesWrappers.Remove(wrapper);
        }

        public void Cleanup()
        {
            foreach (var wrapper in _audioSourcesWrappers) 
                Object.Destroy(wrapper);

            _audioSourcesWrappers.Clear();
        }

        private AudioSourceWrapper AddToPool()
        {
            var wrapper = _audioSourcePool.Spawn();
            _audioSourcesWrappers.Add(wrapper);
            return wrapper;
        }
    }
}
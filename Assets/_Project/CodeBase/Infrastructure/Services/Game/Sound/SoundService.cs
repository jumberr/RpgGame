using System;
using _Project.CodeBase.Infrastructure.Services.Game.Sound;
using _Project.CodeBase.StaticData.Sound;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

namespace _Project.CodeBase.Infrastructure.Services
{
    public class SoundService : IDisposable
    {
        private readonly AudioSourcePool _audioSourcePool;
        private readonly SurfaceSoundConfig _surfaceSoundConfig;


        public SoundService(AudioSourcePool audioSourcePool, SurfaceSoundConfig surfaceSoundConfig)
        {
            _audioSourcePool = audioSourcePool;
            _surfaceSoundConfig = surfaceSoundConfig;
        }

        public void Dispose() => 
            _audioSourcePool.Cleanup();

        public async UniTask PlayMovementSound(SurfaceType surfaceType, MoveType moveType)
        {
            var sounds = _surfaceSoundConfig.GetSounds(surfaceType, moveType);
            if (sounds.Count <= 0) return;

            var audioData = sounds[Random.Range(0, sounds.Count)];
            await PlaySound(audioData);
        }

        private async UniTask PlaySound(AudioData data)
        {
            var audioSourceWrapper = _audioSourcePool.GetAudioSourceWrapper(data);
            Play(data, audioSourceWrapper);

            await UniTask.Delay(TimeSpan.FromSeconds(data.Delay + data.AudioClip.length));
            _audioSourcePool.ReturnToPool(audioSourceWrapper);
        }

        private void Play(AudioData data, AudioSourceWrapper audioSourceWrapper)
        {
            if (data.Delay > 0)
                audioSourceWrapper.AudioSource.PlayDelayed(data.Delay);
            else
                audioSourceWrapper.AudioSource.Play();
        }
    }
}
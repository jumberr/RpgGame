using _Project.CodeBase.StaticData.Sound;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Utils
{
    public class VSFXComponent : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;


        public async UniTask Play(AudioData data)
        {
            _audioSource.SetupAndPlay(data);
            _particleSystem.Play();

            await UniTask.WaitUntil(() => !_audioSource.isPlaying && !_particleSystem.IsAlive(true));
        }
    }
}
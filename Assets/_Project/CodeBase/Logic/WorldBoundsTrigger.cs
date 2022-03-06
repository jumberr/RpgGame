using System;
using System.Threading;
using _Project.CodeBase.Constants;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic
{
    public class WorldBoundsTrigger : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _waitTime;

        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagConstants.Player))
                RemoveHealth(other.gameObject).Forget();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagConstants.Player))
            {
                _tokenSource.Cancel();
                _tokenSource.Dispose();
                _tokenSource = new CancellationTokenSource();
            }
        }

        private async UniTask RemoveHealth(GameObject player)
        {
            var health = player.GetComponent<IHealth>();
            if (health != null)
            {
                while (true)
                {
                    health.Current -= _damage;
                    await UniTask.Delay(TimeSpan.FromSeconds(_waitTime), cancellationToken: _tokenSource.Token);
                }
            }
        }
    }
}
using System;
using _Project.CodeBase.Infrastructure.SaveLoad;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        public BoxCollider Collider;
        [SerializeField] private float _waitTime;

        private ISaveLoadService _saveLoadService;
        private bool _saved;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService) => 
            _saveLoadService = saveLoadService;

        private void OnTriggerEnter(Collider other)
        {
            if (_saved) return;
            _saved = true;
            _saveLoadService.SaveProgress();
            ResetAbilityToSave().Forget();
            Debug.Log("Progress Saved");
        }

        private void OnDrawGizmos()
        {
            if (!Collider)
                return;
            
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
        }

        private async UniTaskVoid ResetAbilityToSave()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_waitTime));
            _saved = false;
        }
    }
}
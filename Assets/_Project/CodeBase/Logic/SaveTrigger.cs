using System.Collections;
using _Project.CodeBase.Infrastructure.SaveLoad;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        public BoxCollider Collider;
        [SerializeField] private float WaitTime;

        private ISaveLoadService _saveLoadService;
        private bool saved;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService) => 
            _saveLoadService = saveLoadService;

        private void OnTriggerEnter(Collider other)
        {
            if (saved) return;
            saved = true;
            _saveLoadService.SaveProgress();
            StartCoroutine(ResetAbilityToSave());
            Debug.Log("Progress Saved");
        }

        private void OnDrawGizmos()
        {
            if (!Collider)
                return;
            
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
        }

        private IEnumerator ResetAbilityToSave()
        {
            yield return new WaitForSecondsRealtime(WaitTime);
            saved = false;
        }
    }
}
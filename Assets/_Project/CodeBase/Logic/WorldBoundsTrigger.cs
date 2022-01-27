using System;
using System.Collections;
using _Project.CodeBase.Constants;
using UnityEngine;

namespace _Project.CodeBase.Logic
{
    public class WorldBoundsTrigger : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float waitTime;
        
        private Coroutine routine;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagConstants.Player)) 
                routine = StartCoroutine(RemoveHealth(other.gameObject));
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagConstants.Player))
            {
                StopCoroutine(routine);
                routine = null;
            }
        }

        private IEnumerator RemoveHealth(GameObject player)
        {
            var health = player.GetComponent<IHealth>();
            if (health != null)
            {
                while (true)
                {
                    health.Current -= damage;
                    yield return new WaitForSecondsRealtime(waitTime);
                }
            }
        }
    }
}
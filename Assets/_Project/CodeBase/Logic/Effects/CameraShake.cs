using System.Collections;
using UnityEngine;

namespace _Project.CodeBase.Logic.Effects
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private Transform camera;

        public void Shake() => 
            StartCoroutine(ShakeOnce(0.1f, 0.005f));

        private IEnumerator ShakeOnce(float duration, float magnitude)
        {
            var originalPos = camera.transform.localPosition;

            var elapsed = 0f;
            while (elapsed < duration)
            {
                var x = Random.Range(-1f, 1f) * magnitude;
                var y = Random.Range(-1f, 1f) * magnitude;

                var newPos = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
                camera.transform.localPosition = newPos;
                elapsed += Time.deltaTime;
                yield return null;
            }

            camera.transform.localPosition = originalPos;
        }
    }
}
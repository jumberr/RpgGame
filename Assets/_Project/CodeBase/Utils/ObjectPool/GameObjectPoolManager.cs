using UnityEngine;

namespace _Project.CodeBase.Utils.ObjectPool
{
    public class GameObjectPoolManager : PoolManager<GameObject>
    {
        public override void OnSpawn(GameObject obj, Vector3 position, Quaternion rotation)
        {
            SetPositionAndRotation(obj, position, rotation);
            SetActive(obj, true);
        }

        public override void OnRelease(GameObject obj) => 
            SetActive(obj, false);

        public override void OnInstantiate(GameObject obj) => 
            SetActive(obj, false);

        private void SetActive(GameObject obj, bool value) => 
            obj.gameObject.SetActive(value);

        private void SetPositionAndRotation(GameObject obj, Vector3 position, Quaternion rotation) => 
            obj.transform.SetPositionAndRotation(position, rotation);
    }
}
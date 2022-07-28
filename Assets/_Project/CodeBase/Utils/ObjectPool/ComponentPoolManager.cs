using UnityEngine;

namespace _Project.CodeBase.Utils.ObjectPool
{
    public class ComponentPoolManager : PoolManager<Component>
    {
        public override void OnSpawn(Component obj, Vector3 position, Quaternion rotation)
        {
            SetPositionAndRotation(obj, position, rotation);
            SetActive(obj, true);
        }

        public override void OnRelease(Component obj) => 
            SetActive(obj, false);

        public override void OnInstantiate(Component obj) => 
            SetActive(obj, false);

        private void SetActive(Component obj, bool value) => 
            obj.gameObject.SetActive(value);

        private void SetPositionAndRotation(Component obj, Vector3 position, Quaternion rotation) => 
            obj.transform.SetPositionAndRotation(position, rotation);
    }
}
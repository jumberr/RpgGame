using UnityEngine;

namespace _Project.CodeBase.Utils
{
    public static class LayerUtils
    {
        public static int ConvertLayerMaskToInt(LayerMask layerMask)
        {
            for (var i = 0; i < 32; i++)
                if (((1 << i) & layerMask.value) > 0) return i;

            return 0;
        }

        public static void SetLayerRecursive(GameObject parentObject, int layer)
        {
            parentObject.layer = layer;

            foreach (Transform child in parentObject.transform) 
                SetLayerRecursive(child.gameObject, layer);
        }
    }
}
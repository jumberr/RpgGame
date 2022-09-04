using UnityEngine;

namespace _Project.CodeBase.Utils
{
    public static class LerpUtils
    {
        public static float Damp(float a, float b, float lambda, float dt) => 
            Mathf.Lerp(a, b, 1 - Mathf.Exp(-lambda * dt));
    }
}
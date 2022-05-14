using UnityEngine;

namespace _Project.CodeBase.Utils
{
    public static class ComponentUtils
    {
        public static void SetActive(bool value, params GameObject[] list)
        {
            foreach (var go in list) 
                go.SetActive(value);
        }
        
        public static void SetActive(bool value, params Component[] list)
        {
            foreach (var go in list) 
                go.gameObject.SetActive(value);
        }
    }
}
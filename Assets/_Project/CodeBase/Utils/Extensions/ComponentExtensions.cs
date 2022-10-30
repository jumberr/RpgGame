using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.Utils.Extensions
{
    public static class ComponentExtensions
    {
        public static void Activate(this Component component) => 
            component.gameObject.SetActive(true);
        
        public static void Deactivate(this Component component) => 
            component.gameObject.SetActive(false);

        public static void ActivateComponents(this List<Component> components)
        {
            foreach (var component in components) 
                component.Activate();
        }
        
        public static void DeactivateComponents(this List<Component> components)
        {
            foreach (var component in components) 
                component.Deactivate();
        }
        
        public static void ActivateComponents(this List<GameObject> components)
        {
            foreach (var component in components) 
                component.SetActive(true);
        }
        
        public static void DeactivateComponents(this List<GameObject> components)
        {
            foreach (var component in components) 
                component.SetActive(false);
        }
    }
}
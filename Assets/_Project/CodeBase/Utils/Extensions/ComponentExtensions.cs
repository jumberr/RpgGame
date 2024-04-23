using System.Collections.Generic;
using _Project.CodeBase.Logic.Hero;
using UnityEngine;
using UnityEngine.UI;

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
        
        public static void EnableComponent(this Behaviour component) => 
            ToggleComponent(component, true);
        
        public static void DisableComponent(this Behaviour component) => 
            ToggleComponent(component, false);
        
        public static void ToggleComponent(this Behaviour component, bool value) => 
            component.enabled = value;
        
        public static void ToggleComponent(this Renderer component, bool value) => 
            component.enabled = value;
    }
}
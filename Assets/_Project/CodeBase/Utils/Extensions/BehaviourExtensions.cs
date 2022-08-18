using UnityEngine;

namespace _Project.CodeBase.Utils.Extensions
{
    public static class BehaviourExtensions
    {
        public static void Enable(this Behaviour component) => 
            component.enabled = true;
        
        public static void Disable(this Behaviour component) => 
            component.enabled = false;
    }
}
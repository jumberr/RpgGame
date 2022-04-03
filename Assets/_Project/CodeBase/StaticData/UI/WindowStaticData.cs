using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.StaticData.UI
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "Static Data/Window Static Data", order = 0)]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}
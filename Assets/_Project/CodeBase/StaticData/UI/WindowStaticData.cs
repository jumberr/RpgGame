using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.StaticData.UI
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "Static Data/UI/Window Static Data")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}
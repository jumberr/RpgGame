using System.Collections.Generic;
using _Project.CodeBase.UI.Services.Windows;
using UnityEngine;

namespace _Project.CodeBase.StaticData.UI
{
    [CreateAssetMenu(fileName = "ExceptionWindows", menuName = "Static Data/UI/Exception Windows")]
    public class ExceptionWindows : ScriptableObject
    {
        public List<WindowId> Windows = new List<WindowId>();
    }
}
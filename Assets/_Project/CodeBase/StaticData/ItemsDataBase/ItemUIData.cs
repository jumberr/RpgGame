using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.StaticData.ItemsDataBase
{
    [Serializable]
    public class ItemUIData
    {
        [BoxGroup("Left", false), LabelWidth(0)]
        public string Name;
        
        [HorizontalGroup("Left/Row", 55)]
        [HideLabel, PreviewField(55, ObjectFieldAlignment.Left)]
        public Sprite Icon;

        [HorizontalGroup("Left/Row", 255), TextArea(3, 3), HideLabel]
        public string Description;
    }
}
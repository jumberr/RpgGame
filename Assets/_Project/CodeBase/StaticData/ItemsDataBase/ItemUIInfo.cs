using System;
using _Project.CodeBase.Logic.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class ItemUIInfo
    {
        [BoxGroup("Left", false), LabelWidth(0)]
        [SerializeField] private ItemName _name;
        
        [HorizontalGroup("Left/Row", 55)]
        [HideLabel, PreviewField(55, ObjectFieldAlignment.Left)]
        [SerializeField] private Sprite _icon;

        [HorizontalGroup("Left/Row", 255), TextArea(3, 3), HideLabel]
        [SerializeField] private string _description;
        
        public ItemName Name => _name;
        public Sprite Icon => _icon;
        public string Description => _description;
    }
}
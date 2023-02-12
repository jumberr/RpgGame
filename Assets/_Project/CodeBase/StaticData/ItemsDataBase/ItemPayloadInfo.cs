using System;
using System.Collections.Generic;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.Logic.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class ItemPayloadInfo
    {
        [BoxGroup("Right", false), EnumToggleButtons, HideLabel]
        [SerializeField] private ItemType _itemType;

        [HorizontalGroup("Right/Row")]
        [SerializeField] private List<ActionType> _actions = new List<ActionType>
        {
            ActionType.Equip,
            ActionType.Use,
            ActionType.Drop,
            ActionType.DropAll
        };
        
        [HorizontalGroup("Right/Row", 55)]
        [HideLabel, PreviewField(55, ObjectFieldAlignment.Right)]
        [SerializeField] private InteractableGroundItem _groundPrefab;
        
        [Range(1, 100), GUIColor(0f, 1f, 0f), BoxGroup("Right", false)]
        [SerializeField] private int _maxInContainer;

        [SerializeField, ReadOnly] private int _dbId = Inventory.ErrorIndex;

        public ItemType ItemType => _itemType;
        public List<ActionType> Actions => _actions;
        public InteractableGroundItem GroundPrefab => _groundPrefab;
        public int MaxInContainer => _maxInContainer;
        public int DbId => _dbId;

        public void SetItemType(ItemType type) => 
            _itemType = type;
        
        public void SetID(int id) => 
            _dbId = id;

        public void SetMaxInContainer(int maxInContainer) => 
            _maxInContainer = maxInContainer;
    }
}
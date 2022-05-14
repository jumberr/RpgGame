using System;
using System.Collections.Generic;
using _Project.CodeBase.Logic.HeroInventory;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.CodeBase.StaticData.ItemsDataBase
{
    [Serializable]
    public class ItemPayloadData
    {
        [HideInInspector]
        public int DbId;
        
        [BoxGroup("Right", false), EnumToggleButtons, HideLabel]
        public ItemType ItemType;

        [HorizontalGroup("Right/Row")]
        public List<ActionType> Actions = new List<ActionType>
        {
            ActionType.Equip,
            ActionType.Use,
            ActionType.Drop,
            ActionType.DropAll
        };
        
        [HorizontalGroup("Right/Row", 55)]
        [HideLabel, PreviewField(55, ObjectFieldAlignment.Right)]
        public GameObject GroundPrefab;
        
        [Range(1, 100), GUIColor(0f, 1f, 0f), BoxGroup("Right", false)]
        public int MaxInContainer;

        
    }
}
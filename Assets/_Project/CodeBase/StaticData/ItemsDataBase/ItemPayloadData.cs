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
        public EItemType ItemType;

        [HorizontalGroup("Right/Row")]
        public List<EActionType> Actions = new List<EActionType>
        {
            EActionType.Equip,
            EActionType.Use,
            EActionType.Drop,
            EActionType.DropAll
        };
        
        [HorizontalGroup("Right/Row", 55)]
        [HideLabel, PreviewField(55, ObjectFieldAlignment.Right)]
        public GameObject Prefab;
        
        [Range(1, 100), GUIColor(0f, 1f, 0f), BoxGroup("Right", false)]
        public int MaxInContainer;

        
    }
}
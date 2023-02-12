using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    public abstract class ItemInfo : SerializedScriptableObject
    {
        [HideLabel, HorizontalGroup("Split", 300)]
        [SerializeField] private ItemUIInfo _uiInfo;
        
        [HideLabel, HorizontalGroup("Split", 300)]
        [SerializeField] private ItemPayloadInfo _payloadInfo;

        public ItemUIInfo UIInfo => _uiInfo;
        public ItemPayloadInfo PayloadInfo => _payloadInfo;

        public int ID => PayloadInfo.DbId;
    }
}                                                 
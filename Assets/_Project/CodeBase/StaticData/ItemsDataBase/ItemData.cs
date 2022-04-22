using Sirenix.OdinInspector;

namespace _Project.CodeBase.StaticData.ItemsDataBase
{
    public abstract class ItemData : SerializedScriptableObject
    {
        [HideLabel, HorizontalGroup("Split", 300)]
        public ItemUIData ItemUIData;
        
        [HideLabel, HorizontalGroup("Split", 300)]
        public ItemPayloadData ItemPayloadData;
    }
}                                                 
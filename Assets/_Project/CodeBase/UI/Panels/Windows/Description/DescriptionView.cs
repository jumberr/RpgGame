using _Project.CodeBase.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI
{
    public class DescriptionView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;

        public Image Icon => _icon;
        public TMP_Text Name => _name;
        public TMP_Text Description => _description;
        
        public void Setup(ItemInfo info)
        {
            _icon.sprite = info.UIInfo.Icon;
            _name.text = info.UIInfo.Name.ToString();
            _description.text = info.UIInfo.Description;
        }
    }
}
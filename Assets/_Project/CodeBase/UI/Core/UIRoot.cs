using _Project.CodeBase.Utils.Factory;
using UnityEngine;

namespace _Project.CodeBase.UI.Core
{
    public class UIRoot : MonoBehaviour
    {
        public class Factory : ComponentPlaceholderFactory<UIRoot>
        {
        }
    }
}
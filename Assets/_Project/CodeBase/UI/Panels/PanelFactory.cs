using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI.Panels
{
    public class PanelFactory
    {
        protected readonly DiContainer Container;

        protected PanelFactory(DiContainer container) => 
            Container = container;

        protected void ResizeWindow(Transform window)
        {
            var rectTransform = (RectTransform) window.transform;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.offsetMin = Vector2.zero;
        }
    }
}
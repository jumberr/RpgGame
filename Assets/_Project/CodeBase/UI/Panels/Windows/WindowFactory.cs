using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.StaticData.UI;
using _Project.CodeBase.UI.Panels;
using _Project.CodeBase.UI.Services.Windows;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase
{
    public class WindowFactory : PanelFactory, IFactory<WindowId, Transform, WindowBase>
    {
        private readonly IWindowService _windowService;
        private readonly IStaticDataService _staticDataService;

        private WindowConfig _currentConfig;

        public WindowFactory(DiContainer container, IWindowService windowService, IStaticDataService staticDataService) : base(container)
        {
            _windowService = windowService;
            _staticDataService = staticDataService;
        }

        public WindowBase Create(WindowId id, Transform parent)
        {
            var cfg = _staticDataService.ForWindow(id);
            var window = Container.InstantiatePrefabForComponent<WindowBase>(cfg.Prefab, parent);
            ResizeWindow(window.transform);
            
            _windowService.AddWindow(window, id);
            return window;
        }
    }
}
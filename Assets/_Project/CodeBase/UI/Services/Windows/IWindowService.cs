using System;
using _Project.CodeBase.UI.Windows;

namespace _Project.CodeBase.UI.Services.Windows
{
    public interface IWindowService
    {
        event Action<WindowId, WindowId> OnSwitchingScreens;
        event Action<WindowId> OnShowingScreen;
        WindowId CurrentWindowId { get; }
        void Setup();
        void Show(WindowId id);
        void Back();
        WindowBase GetWindow(WindowId id);
        void AddWindow(WindowBase window, WindowId id);
    }
}
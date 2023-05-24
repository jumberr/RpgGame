using System;
using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.StaticData.UI;
using _Project.CodeBase.UI.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly InputService _inputService;

        private readonly Dictionary<WindowId, WindowBase> _windows = new Dictionary<WindowId, WindowBase>();
        private readonly List<WindowId> _backList = new List<WindowId>();
        private readonly ExceptionWindows _exceptionWindows;
        private WindowBase _currentWindow;
        private bool _isShown;

        public event Action<WindowId, WindowId> OnSwitchingScreens;
        public event Action<WindowId> OnShowingScreen;

        public WindowId CurrentWindowId => _currentWindow == null ? WindowId.Unknown : _currentWindow.WindowId;

        public WindowService(InputService inputService, IStaticDataService staticDataService)
        {
            _inputService = inputService;
            _exceptionWindows = staticDataService.ForWindowService();
            Subscribe();
        }
        
        ~WindowService() => 
            Cleanup();

        public void Show(WindowId id)
        {
            if (CurrentWindowId == id || _isShown || _backList.Contains(id)) return;
            OpenWindow(id, false).Forget();
        }

        public void Back()
        {
            if (_backList.Count <= 0)
            {
                HideCurrentOrOpenMenu();
                return;
            }

            var id = _backList[_backList.Count - 1];
            if (CurrentWindowId == id) return;
            _backList.RemoveAt(_backList.Count - 1);
            
            OpenWindow(id, true).Forget();
        }

        public WindowBase GetWindow(WindowId id) => 
            _windows[id];

        public void AddWindow(WindowBase window, WindowId id) => 
            _windows.Add(id, window);

        private void Subscribe()
        {
            _inputService.BackAction.Event += Back;
            _inputService.InventoryAction.Event += ShowInventory;
        }

        private void Cleanup()
        {
            _inputService.BackAction.Event -= Back;
            _inputService.InventoryAction.Event -= ShowInventory;
        }

        private void ShowSettings() => 
            Show(WindowId.Settings);

        private void HideCurrentOrOpenMenu()
        {
            if (_currentWindow is null)
                ShowSettings();
            else
            {
                _currentWindow.Hide().Forget();
                _currentWindow = null;
                _isShown = false;
            }
        }

        private void ShowInventory() => 
            Show(WindowId.Inventory);
        
        private async UniTask OpenWindow(WindowId id, bool back)
        {
            _isShown = true;
            Debug.Log("[ScreenController] Showing screen " + id);

            var window = GetWindow(id);

            if (window == null)
            {
                Debug.LogError("[ScreenController] Could not find screen with the given screenId: " + id);
                return;
            }

            if (_currentWindow != null)
            {
                await _currentWindow.Hide();

                if (!back && !_exceptionWindows.Windows.Contains(_currentWindow.WindowId))
                    _backList.Add(_currentWindow.WindowId);

                OnSwitchingScreens?.Invoke(_currentWindow.WindowId, id);
            }

            await window.Show();
            _currentWindow = window;
            OnShowingScreen?.Invoke(id);
            _isShown = false;
        }
    }
}
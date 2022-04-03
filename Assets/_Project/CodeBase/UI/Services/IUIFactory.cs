using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.UI.Services
{
    public interface IUIFactory
    {
        UniTask CreateUIRoot();
        UniTask<GameObject> CreateHud();
        void CreateDeathScreen();
        void CreateInventory(GameObject hero);
        void SetupWindowButtons(IWindowService windowService);

        void OpenInventory();
    }
}
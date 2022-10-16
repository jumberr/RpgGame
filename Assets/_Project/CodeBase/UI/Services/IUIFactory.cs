using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.UI.Services.Windows;
using _Project.CodeBase.UI.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.UI.Services
{
    public interface IUIFactory
    {
        UniTask CreateUIRoot();
        UniTask CreateHud();
        void CreateDeathScreen();
        void CreateInventory();
        GameObject CreateSettings(HeroRotation rotation);
        void ConstructHud(HeroFacade facade);
        void ConstructInventoriesHolder(HeroFacade facade);

        void OpenWindow(WindowId id);
        void HideWindow(WindowId id);
        WindowBase GetWindow(WindowId id);
    }
}
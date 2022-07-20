using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.UI.Services
{
    public interface IUIFactory
    {
        UniTask CreateUIRoot();
        UniTask CreateHud();
        void CreateDeathScreen();
        void CreateInventory(GameObject hero);
        GameObject CreateSettings(HeroRotation rotation);
        void ConstructActorUI(GameObject hero);

        void OpenWindow(WindowId id);
        void HideWindow(WindowId id);
    }
}
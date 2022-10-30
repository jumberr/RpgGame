using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Hero.Cam;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.UI.Services
{
    public interface IUIFactory
    {
        UniTask CreateUIRoot();
        UniTask CreateHud();
        void SetupWindowService();
        void CreateDeathScreen();
        void CreateInventory();
        GameObject CreateSettings(HeroCamera camera);
        void ConstructHud(HeroFacade facade);
        void ConstructInventoriesHolder(HeroFacade facade);
    }
}
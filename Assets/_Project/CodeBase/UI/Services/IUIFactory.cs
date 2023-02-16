using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.UI.Windows.Settings;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.UI.Services
{
    public interface IUIFactory
    {
        UniTask CreateUIRoot();
        UniTask CreateHud();
        void CreateDeathScreen();
        void CreateInventory();
        SettingsUI CreateSettings();
        void ConstructInventoriesHolder(HeroFacade facade);
    }
}
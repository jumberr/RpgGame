using _Project.CodeBase.Logic.Hero;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.UI.Services
{
    public interface IUIFactory
    {
        UniTask CreateUI();
        void ShowDeathScreen();
        void ConstructInventoriesHolder(HeroFacade facade);
    }
}
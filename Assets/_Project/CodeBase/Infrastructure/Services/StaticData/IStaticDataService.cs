using _Project.CodeBase.StaticData;
using _Project.CodeBase.StaticData.ItemsDataBase;
using _Project.CodeBase.StaticData.UI;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        UniTask LoadMenuStaticData();
        UniTask LoadGameStaticData();
        UniTask LoadItemsDataBase();
        UniTask LoadUIWindowConfig();
        PlayerStaticData ForPlayer();
        ProjectSettings ForProjectSettings();
        ItemsDataBase ForInventory();
        WindowConfig ForWindow(WindowId windowId);
    }
}
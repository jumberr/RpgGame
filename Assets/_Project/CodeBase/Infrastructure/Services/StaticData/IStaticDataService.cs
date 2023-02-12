using _Project.CodeBase.StaticData;
using _Project.CodeBase.StaticData.UI;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        UniTask LoadProjectConfig();
        UniTask LoadGameStaticData();
        UniTask LoadItemsDataBase();
        UniTask LoadUIWindowConfig();
        UniTask LoadExceptionWindows();
        ProjectSettings ForProjectSettings();
        ExceptionWindows ForWindowService();
        PlayerStaticData ForPlayer();
        ItemsInfo ForInventory();
        WindowConfig ForWindow(WindowId windowId);
    }
}
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Services
{
    public interface IStaticDataService
    {
        UniTask LoadMenuStaticData();
        UniTask LoadGameStaticData();
        PlayerStaticData ForPlayer();
        ProjectSettings ForProjectSettings();
    }
}
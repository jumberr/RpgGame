using _Project.CodeBase.Logic.Hero.State;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Logic.Hero.Reload
{
    public interface IReloadSystem
    {
        UniTask Reload((ReloadState, int, int) result);
    }
}
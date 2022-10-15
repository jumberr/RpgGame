using _Project.CodeBase.Infrastructure.States;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure
{
    public interface IGameStateMachine
    {
        UniTask Enter<TState>() where TState : class, IState;
        TState ChangeState<TState>() where TState : class, IExitableState;
    }
}
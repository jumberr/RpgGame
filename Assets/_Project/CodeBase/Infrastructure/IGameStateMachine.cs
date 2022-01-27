using _Project.CodeBase.Infrastructure.States;

namespace _Project.CodeBase.Infrastructure
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : class, IState;
        TState ChangeState<TState>() where TState : class, IExitableState;
    }
}
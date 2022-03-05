namespace _Project.CodeBase.Logic.Hero.Animation
{
    public interface IAnimationStateMachine
    {
        void Enter<TState>() where TState : class, IAnimationState;
    }
}
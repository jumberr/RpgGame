namespace _Project.CodeBase.Logic.Hero.Animation
{
    public interface IAnimationStateMachine
    {
        EAnimationState GetStateName();
        void Enter<TState>() where TState : class, IAnimationState;
    }
}
namespace _Project.CodeBase.Logic.Enemy.FSM
{
    public interface IAIState
    {
        AIStateName StateName { get; }
        void Enter(AIAgent agent);
        void Update(AIAgent agent);
        void Exit(AIAgent agent);
    }
}
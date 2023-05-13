namespace _Project.CodeBase.Logic.Enemy.FSM.States
{
    public class DeathState : IAIState
    {
        public AIStateName StateName => AIStateName.Death;
        
        public void Enter(AIAgent agent)
        {
            agent.NavMeshAgent.ResetPath();
            agent.EnemyAnimator.PlayDeathAnimation();
        }

        public void Update(AIAgent agent)
        {
        }

        public void Exit(AIAgent agent)
        {
        }
    }
}
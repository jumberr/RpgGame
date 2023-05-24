using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.Infrastructure.Services.StaticData;

namespace _Project.CodeBase.Logic.Enemy.FSM
{
    public class AIStateMachine
    {
        private readonly List<IAIState> _states = new List<IAIState>();
        private AIAgent _agent;
        private AIStateName _currentState;

        public AIStateMachine(List<IAIState> allStates, IStaticDataService staticDataService)
        {
            var enemyStaticData = staticDataService.ForEnemy();
            PrepareStates(allStates, enemyStaticData.States);
        }

        public void SetAIAgent(AIAgent agent) => 
            _agent = agent;

        public void RegisterState(IAIState state) => 
            _states.Add(state);

        public IAIState GetState(AIStateName name) => 
            _states.FirstOrDefault(state => name == state.StateName);

        public void Update() => 
            GetState(_currentState)?.Update(_agent);

        public void ChangeState(AIStateName newState)
        {
            if (newState == _currentState) return;

            GetState(_currentState)?.Exit(_agent);
            _currentState = newState;
            GetState(_currentState).Enter(_agent);
        }
        
        private void PrepareStates(List<IAIState> allStates, List<AIStateName> availableStates)
        {
            foreach (var state in availableStates) 
                RegisterState(allStates.Find(x => x.StateName == state));
        }
    }
}
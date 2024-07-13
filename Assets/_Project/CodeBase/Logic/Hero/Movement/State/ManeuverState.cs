using _Project.CodeBase.Infrastructure.Factory;
using Better.StateMachine.Runtime.States;

namespace _Project.CodeBase.Logic.Hero
{
    public abstract class ManeuverState : BaseState
    {
        protected VelocityController VelocityController;
        protected MovementData MovementData;
        private IGameFactory _factory;


        public void Init(
            IGameFactory factory,
            VelocityController velocityController,
            MovementData movementData)
        {
            _factory = factory;
            VelocityController = velocityController;
            MovementData = movementData;
            _factory.HeroCreated += OnHeroCreated;
        }

        public virtual void Dispose() => 
            _factory.HeroCreated -= OnHeroCreated;

        public abstract bool CanEnter();

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        protected virtual void OnHeroCreated(HeroFacade facade)
        {
        }
    }
}
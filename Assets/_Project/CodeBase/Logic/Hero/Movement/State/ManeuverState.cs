using Better.StateMachine.Runtime.States;

namespace _Project.CodeBase.Logic.Hero
{
    public abstract class ManeuverState : BaseState
    {
        protected VelocityController VelocityController;
        protected FootstepsController FootstepsController;
        protected MovementData MovementData;


        public void Init(
            VelocityController velocityController,
            FootstepsController footstepsController,
            MovementData movementData)
        {
            VelocityController = velocityController;
            FootstepsController = footstepsController;
            MovementData = movementData;
        }

        public abstract bool CanEnter();

        public virtual void Update()
        {
        }
        
        public virtual void FixedUpdate()
        {
        }
    }
}
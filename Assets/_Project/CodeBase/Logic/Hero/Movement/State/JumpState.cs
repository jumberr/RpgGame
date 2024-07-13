using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class JumpState : ManeuverState
    {
        private const float FormulaValue = 2f;
        private FallingDamage _fallingDamage;
        private PlayerMonoComponentsProvider _componentsProvider;
        private FootstepsController _footstepsController;
        private bool _initialized;


        public override bool CanEnter()
        {
            //if (!_componentsProvider.IsGrounded || _state.Crouching && !MovementData.СanJumpWhileCrouching) return false;  // todo
            if (!_componentsProvider.IsGrounded) return false;
            return true;
        }

        public override async Task EnterAsync(CancellationToken token)
        {
            VelocityController.SetVelocityY(Mathf.Sqrt(FormulaValue * MovementData.JumpForce / MovementData.Gravity)); // todo check, it was FormulaValue * MovementData.JumpForce * MovementData.JumpGravity
        }

        public override async Task ExitAsync(CancellationToken token)
        {
            if (!_initialized) return;
            
            _fallingDamage.ApplyFallDamage(_componentsProvider.CachedTransform.position.y);
            _fallingDamage.ResetHighestPoint();
            _footstepsController.HandleJumpFootsteps();
        }

        protected override void OnHeroCreated(HeroFacade facade)
        {
            _fallingDamage = facade.FallingDamage;
            _componentsProvider = facade.ComponentsProvider;
            _footstepsController = facade.FootstepsController;
            
            _initialized = true;
        }
    }
}
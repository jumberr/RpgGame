using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class JumpState : ManeuverState
    {
        private const float FormulaValue = 2f;
        private readonly FallingDamage _fallingDamage;
        private PlayerMonoComponentsProvider _componentsProvider;

        
        public JumpState(FallingDamage fallingDamage)
        {
            _fallingDamage = fallingDamage;
        }

        public override bool CanEnter()
        {
            return true; // todo
        }

        public override async Task EnterAsync(CancellationToken token)
        {
            VelocityController.SetVelocityY(Mathf.Sqrt(FormulaValue * MovementData.JumpForce / MovementData.Gravity)); // todo check, it was FormulaValue * MovementData.JumpForce * MovementData.JumpGravity
        }

        public override async Task ExitAsync(CancellationToken token)
        {
            _fallingDamage.ApplyFallDamage(_componentsProvider.CachedTransform.position.y);
            _fallingDamage.ResetHighestPoint();
            FootstepsController.HandleJumpFootsteps();
        }
    }
}
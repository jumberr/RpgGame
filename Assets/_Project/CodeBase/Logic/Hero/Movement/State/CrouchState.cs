using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class CrouchState : ManeuverState
    {
        private PlayerMonoComponentsProvider _componentsProvider;

        
        public override bool CanEnter()
        {
            if (!MovementData.CanCrouch) return false;
            
            var sphereLocation = _componentsProvider.CachedTransform.position + Vector3.up * _standingHeight;
            return Physics.OverlapSphereNonAlloc(sphereLocation, _componentsProvider.CharacterController.radius, _buffer, _crouchOverlapsMask) == 0;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            
        }

        public override Task ExitAsync(CancellationToken token)
        {
            
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class CrouchState : ManeuverState
    {
        private PlayerMonoComponentsProvider _componentsProvider;
        private readonly Collider[] _buffer = new Collider[5];

        
        public override bool CanEnter()
        {
            if (!MovementData.CanCrouch) return false;
            
            var sphereLocation = _componentsProvider.CachedTransform.position + Vector3.up * _componentsProvider.StandingHeight;
            return Physics.OverlapSphereNonAlloc(sphereLocation, _componentsProvider.CharacterController.radius, _buffer, MovementData.СrouchOverlapsMask) == 0;
        }

        public override async Task EnterAsync(CancellationToken token)
        {
            
        }

        public override async Task ExitAsync(CancellationToken token)
        {
            
        }
    }
}
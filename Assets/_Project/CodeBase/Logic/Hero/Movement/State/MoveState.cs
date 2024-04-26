using System;
using System.Threading;
using System.Threading.Tasks;
using _Project.CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class MoveState : ManeuverState, IDisposable
    {
        private InputService _inputService;
        private PlayerMonoComponentsProvider _componentsProvider;
        private Vector2 _input;

        public MoveState(InputService inputService)
        {
            _inputService = inputService;
            _inputService.MoveAction.Event += UpdateDirection;
        }

        public void Dispose()
        {
            _inputService.MoveAction.Event -= UpdateDirection;
        }

        public override bool CanEnter()
        {
            
        }

        public override Task EnterAsync(CancellationToken token)
        {
            
        }

        public override Task ExitAsync(CancellationToken token)
        {
            
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            Vector2 frameInput = Vector3.ClampMagnitude(_input, 1f);
            var desiredDirection = CalculateDirection(frameInput);

            CalculateVelocity(desiredDirection);

            var applied = ApplyVelocity();
            _componentsProvider.CharacterController.Move(applied);
            ApplyAnimation(applied);
            _footstepsController.HandleMoveFootsteps(frameInput);
        }

        private void UpdateDirection(Vector2 input) => 
            _input = input;

        private Vector3 CalculateDirection(Vector2 frameInput)
        {
            var desiredDirection = new Vector3(frameInput.x, 0f, frameInput.y);
            desiredDirection = ApplySpeed(frameInput, desiredDirection);
            return _componentsProvider.CachedTransform.TransformDirection(desiredDirection);
        }

        private void CalculateVelocity(Vector3 desiredDirection)
        {
            if (!_componentsProvider.IsGrounded)
            {
                VelocityController.ResetVelocity();
                _velocity += desiredDirection * _accelerationInAir * _airControl * Time.deltaTime;
                _velocity.y -= (_velocity.y >= 0 ? _jumpGravity : _gravity) * Time.deltaTime;
            }
            else if (!_state.Jumping)
                _velocity = Vector3.Lerp(_velocity, new Vector3(desiredDirection.x, _velocity.y, desiredDirection.z), Time.deltaTime * (desiredDirection.sqrMagnitude > 0.0f ? _acceleration : _deceleration));
        }
        
        private Vector3 ApplyVelocity()
        {
            var applied = VelocityController.Velocity * Time.deltaTime;
            if (_componentsProvider.IsGrounded && !_state.Jumping)
                applied.y = - MovementData.StickToGroundForce;
            return applied;
        }
        
        private Vector3 ApplySpeed(Vector2 frameInput, Vector3 desiredDirection)
        {
            if (_state.Running)
                desiredDirection *= MovementData.SpeedRunning;
            else if (_state.Crouching)
                desiredDirection *= MovementData.SpeedCrouching;
            else if (_state.Aiming)
                desiredDirection *= MovementData.SpeedAiming;
            else
            {
                desiredDirection *= MovementData.SpeedWalking;
                desiredDirection.x *= MovementData.WalkingMultiplierSideways;
                desiredDirection.z *= frameInput.y > 0 ? MovementData.WalkingMultiplierForward : MovementData.WalkingMultiplierBackwards;
            }
            
            return desiredDirection;
        }
    }
}
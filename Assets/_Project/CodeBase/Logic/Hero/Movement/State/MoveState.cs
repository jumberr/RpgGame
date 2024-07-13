using System;
using System.Threading;
using System.Threading.Tasks;
using _Project.CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class MoveState : ManeuverState, IDisposable
    {
        private readonly InputService _inputService;
        private PlayerMonoComponentsProvider _componentsProvider;
        private HeroAnimator _heroAnimator;
        private FootstepsController _footstepsController;
        private Vector2 _input;
        private bool _isRunning;
        private bool _initialized;


        public MoveState(InputService inputService)
        {
            _inputService = inputService;
            _inputService.MoveAction.Event += UpdateDirection;
            _inputService.RunningAction.Event += Run;
        }

        public override void Dispose()
        {
            base.Dispose();
            _inputService.MoveAction.Event -= UpdateDirection;
            _inputService.RunningAction.Event -= Run;
        }

        public override bool CanEnter()
        {
            return true;
        }

        public override async Task EnterAsync(CancellationToken token)
        {
            
        }

        public override async Task ExitAsync(CancellationToken token)
        {
            
        }

        public override void FixedUpdate()
        {
            if (!_initialized) return;
            
            base.FixedUpdate();
            
            Vector2 frameInput = Vector3.ClampMagnitude(_input, 1f);
            var desiredDirection = CalculateDirection(frameInput);

            CalculateVelocity(desiredDirection);

            var applied = ApplyVelocity();
            _componentsProvider.CharacterController.Move(applied);
            ApplyAnimation(applied);
            _footstepsController.HandleMoveFootsteps(frameInput);
        }

        protected override void OnHeroCreated(HeroFacade facade)
        {
            _componentsProvider = facade.ComponentsProvider;
            _heroAnimator = facade.Animator;
            _footstepsController = facade.FootstepsController;

            _initialized = true;

            // todo
        }

        private void UpdateDirection(Vector2 input) => 
            _input = input;
        
        private void Run(bool isRunning) => 
            _isRunning = isRunning;

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
                VelocityController.AddVelocity(desiredDirection *
                                               (MovementData.AccelerationInAir * MovementData.AirControl *
                                                Time.deltaTime));
                var yVelocity = (VelocityController.Velocity.y >= 0 ? MovementData.JumpGravity : MovementData.Gravity) *
                                Time.deltaTime;
                VelocityController.RemoveVelocity(new Vector3(0, yVelocity, 0));
            }
            //else if (!_state.Jumping) todo
            else
            {
                VelocityController.SetVelocity(Vector3.Lerp(VelocityController.Velocity, 
                    new Vector3(desiredDirection.x, VelocityController.Velocity.y, desiredDirection.z),
                    Time.deltaTime * (desiredDirection.sqrMagnitude > 0f ? MovementData.Acceleration : MovementData.Deceleration))); 
            }
        }

        private Vector3 ApplyVelocity()
        {
            var applied = VelocityController.Velocity * Time.deltaTime;
            //if (_componentsProvider.IsGrounded && !_state.Jumping) todo
            if (_componentsProvider.IsGrounded)
                applied.y = - MovementData.StickToGroundForce;
            return applied;
        }
        
        private Vector3 ApplySpeed(Vector2 frameInput, Vector3 desiredDirection) // todo
        {
            //if (_isRunning)
            //    desiredDirection *= MovementData.SpeedRunning;
            //else if (_state.Crouching)
            //    desiredDirection *= MovementData.SpeedCrouching;
            //else if (_state.Aiming)
            //    desiredDirection *= MovementData.SpeedAiming;
            //else
            //{
                desiredDirection *= MovementData.SpeedWalking;
                desiredDirection.x *= MovementData.WalkingMultiplierSideways;
                desiredDirection.z *= frameInput.y > 0 ? MovementData.WalkingMultiplierForward : MovementData.WalkingMultiplierBackwards;
            //}
            
            return desiredDirection;
        }
        
        private void ApplyAnimation(Vector3 dir)
        {
            dir.y = 0f;
            _heroAnimator.EnterMoveState(_isRunning, dir);
        }
    }
}
using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    [Serializable]
    public struct MovementData
    {
        [Header("Acceleration")]
        public float Acceleration;
        public float AccelerationInAir;
        public float Deceleration;
        
        [Header("Air")]
        [SerializeField, Range(0.0f, 1.0f)] public float AirControl;
        public float Gravity;
        
        [Header("Move Speed")]
        public float SpeedWalking;
        public float SpeedAiming;
        public float SpeedCrouching;
        public float SpeedRunning;
        
        [Header("Walking Multipliers")]
        [Range(0.0f, 1.0f)] public float WalkingMultiplierForward;
        [Range(0.0f, 1.0f)] public float WalkingMultiplierSideways;
        [Range(0.0f, 1.0f)] public float WalkingMultiplierBackwards;
        
        public float StickToGroundForce;
        
        [Header("Jump")]
        public float JumpForce;
        public float JumpGravity;
        
        [Header("Crouching")]
        public bool CanCrouch;
        [ShowIf(nameof(CanCrouch))] public bool СanCrouchWhileFalling;
        [ShowIf(nameof(CanCrouch))] public bool СanJumpWhileCrouching;
        [ShowIf(nameof(CanCrouch))] public float СrouchHeight;
        [ShowIf(nameof(CanCrouch))] public LayerMask СrouchOverlapsMask;
    }
}
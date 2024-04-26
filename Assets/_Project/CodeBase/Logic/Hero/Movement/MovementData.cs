using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    [Serializable]
    public struct MovementData
    {
        public float Gravity; //35
        
        [Header("Move Speed")]
        public float SpeedWalking; // 3.5
        public float SpeedAiming; // 3
        public float SpeedCrouching; // 2
        public float SpeedRunning; // 6.8
        
        [Header("Walking Multipliers")]
        [Range(0.0f, 1.0f)] public float WalkingMultiplierForward; // 1
        [Range(0.0f, 1.0f)] public float WalkingMultiplierSideways; // 0.9
        [Range(0.0f, 1.0f)] public float WalkingMultiplierBackwards; // 0.9
        
        public float StickToGroundForce; // 0.3
        
        [Header("Jump")]
        public float JumpForce;//1.5
        public float JumpGravity; //35
        
        [Header("Crouching")]
        public bool CanCrouch;
        [ShowIf(nameof(CanCrouch))] public bool СanCrouchWhileFalling;
        [ShowIf(nameof(CanCrouch))] public bool СanJumpWhileCrouching;
        [ShowIf(nameof(CanCrouch))] public float СrouchHeight;
        [ShowIf(nameof(CanCrouch))] public LayerMask СrouchOverlapsMask;
    }
}
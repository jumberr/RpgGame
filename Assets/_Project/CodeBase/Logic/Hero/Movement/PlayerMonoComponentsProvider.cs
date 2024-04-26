using NTC.Global.Cache;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class PlayerMonoComponentsProvider : NightCache
    {
        public bool IsGrounded  => CharacterController.isGrounded;
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
    }
}
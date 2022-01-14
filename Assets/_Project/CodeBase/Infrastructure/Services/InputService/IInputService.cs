using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.InputService
{
    public interface IInputService
    {
        Vector2 MoveAxis { get; }
        Vector2 RotationAxis { get; }
        bool IsAttackButtonUp();
        bool IsJumpAction();
    }
}
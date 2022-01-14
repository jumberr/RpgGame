using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.InputService
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";

        protected const string CamHorizontal = "CameraHorizontal";
        protected const string CamVertical = "CameraVertical";

        private const string Fire = "Fire";
        private const string Jump = "Jump";

        public abstract Vector2 MoveAxis { get; }
        public abstract Vector2 RotationAxis { get; }

        public bool IsAttackButtonUp() =>
            SimpleInput.GetButtonUp(Fire);

        protected static Vector2 SimpleInputAxis() =>
            new Vector2(SimpleInput.GetAxisRaw(Horizontal), SimpleInput.GetAxisRaw(Vertical));

        protected static Vector2 RotationInputAxis() =>
            new Vector2(SimpleInput.GetAxisRaw(CamHorizontal), SimpleInput.GetAxisRaw(CamVertical));

        public bool IsJumpAction() =>
            SimpleInput.GetButtonUp(Jump);
    }
}
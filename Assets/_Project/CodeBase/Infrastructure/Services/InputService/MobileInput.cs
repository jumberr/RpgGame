using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.InputService
{
    public class MobileInput : InputService
    {
        public override Vector2 MoveAxis => SimpleInputAxis();
        public override Vector2 RotationAxis => RotationInputAxis();
    }
}
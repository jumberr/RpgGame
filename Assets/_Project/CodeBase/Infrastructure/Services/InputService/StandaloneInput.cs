using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.InputService
{
    public class StandaloneInput : InputService
    {
        public override Vector2 MoveAxis
        {
            get
            {
                var axis = SimpleInputAxis();

                if (axis == Vector2.zero)
                    axis = UnityAxis();
                return axis;
            }
        }
        
        public override Vector2 RotationAxis => RotationInputAxis();

        private static Vector2 UnityAxis() =>
            new Vector2(Input.GetAxisRaw(Horizontal), Input.GetAxisRaw(Vertical));
    }
}
using UnityEngine;

namespace Code.Scripts
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerMovement _playerMovement;
        private int horizontal = Animator.StringToHash("Horizontal");
        private int vertical = Animator.StringToHash("Vertical");

        public void Start()
        {
            _playerMovement.OnMove += IdleMoveRunAnimate;
        }

        private void IdleMoveRunAnimate(Vector2 input)
        {
            var hor = 0;
            var vert = Mathf.Clamp01(Mathf.Abs(input.x) + Mathf.Abs(input.y));
            var snappedHorizontal = SnappingForMovement(hor);
            var snappedVertical = SnappingForMovement(vert);

            _animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
            _animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
        }

        private float SnappingForMovement(float value)
        {
            float snappedValue;
            if (value > 0 && value < 0.55f)
                snappedValue = 0.5f;
            else if (value > 0.55f)
                snappedValue = 1;
            else if (value < 0 && value > - 0.55f)
                snappedValue = -0.5f;
            else if (value < -0.55f)
                snappedValue = -1f;
            else
                snappedValue = 0;

            return snappedValue;
        }
    }
}
using System;
using UnityEngine;

namespace Code.Scripts
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerMovement _playerMovement;
        private readonly int horizontal = Animator.StringToHash("Horizontal");
        private readonly int vertical = Animator.StringToHash("Vertical");

        private void Start()
        {
            _playerMovement.OnMove += IdleMoveRunAnimate;
        }

        private void IdleMoveRunAnimate(float clampedInput, bool sprint)
        {
            var hor = 0;
            var snappedHorizontal = SnappingForMovement(hor);
            var snappedVertical = SnappingForMovement(clampedInput);

            if (sprint)
            {
                snappedHorizontal = hor;
                snappedVertical = 2;
            }

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
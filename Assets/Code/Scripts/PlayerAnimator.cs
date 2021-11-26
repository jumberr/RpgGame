using System.Collections;
using UnityEngine;

namespace Code.Scripts
{
    public delegate void CustomCallback();
    
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerMovement _playerMovement;
        private readonly int horizontal = Animator.StringToHash("Horizontal");
        private readonly int vertical = Animator.StringToHash("Vertical");

        public static int Jump = Animator.StringToHash("Jump");
        public static int Roll = Animator.StringToHash("Roll");
        
        private void Start()
        {
            _playerMovement.OnMove += IdleMoveRunAnimate;
        }

        public void PlayTargetAnimation(int animHash, bool applyRootMotion, params CustomCallback[] callbacks)
        {
            _animator.applyRootMotion = applyRootMotion;
            _animator.CrossFade(animHash, 0.2f);
            StartCoroutine(OnEndAnimation(callbacks));
        }

        private IEnumerator OnEndAnimation(params CustomCallback[] callbacks)
        {
            var delay = 1.2f;
            var length = _animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSecondsRealtime(length * delay);
            _playerMovement.InAction = false;
            _animator.applyRootMotion = false;
            foreach (var i in callbacks)
            {
                i?.Invoke();
            }
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
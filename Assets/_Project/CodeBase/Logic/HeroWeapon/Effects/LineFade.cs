using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon.Effects
{
    public class LineFade : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField] private Color _color;
        [SerializeField] private float _speed;
        
        private void OnEnable() => 
            _color.a = 255;

        private void Update()
        {
            _color.a = Mathf.Lerp(_color.a, 0, Time.deltaTime * _speed);
            _line.startColor = _color;
            _line.endColor = _color;
        }

        public void SetPositions(Vector3 start, Vector3 end)
        {
            _line.SetPosition(0, start);
            _line.SetPosition(1, end);
        }
    }
}

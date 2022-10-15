using DG.Tweening;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon.Effects
{
    public class LineFade : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField] private Color _color;
        [SerializeField] private float _speed;

        private Color2 _startColor;
        private Color2 _endColor;
        
        public void Start()
        {
            var endColor = new Color(_color.r, _color.g, _color.b, 0);
            _startColor = new Color2(_color, _color);
            _endColor = new Color2(endColor, endColor);
        }
        
        private void OnEnable() => 
            _line.DOColor(_startColor, _endColor, _speed);

        public void SetPositions(Vector3 start, Vector3 end)
        {
            _line.SetPosition(0, start);
            _line.SetPosition(1, end);
        }
    }
}

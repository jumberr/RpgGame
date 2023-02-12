using DG.Tweening;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon.Data
{
    public class SniperScopeRender : MonoBehaviour
    {
        private const int Zero = 0;
        private const int One = 1;
        
        [SerializeField] private SpriteRenderer _crossHair;
        [SerializeField] private Material _material;

        public void Turn(bool value, float time) => 
            Turn(value ? One : Zero, time);

        private void Turn(float value, float time)
        {
            _crossHair.DOFade(value, time);
            _material.DOFade(value, time);
        }
    }
}
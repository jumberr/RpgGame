using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon.Effects
{
    public class WeaponLight : MonoBehaviour
    {
        private GameObject _light;

        public void Construct(GameObject light) => 
            _light = light;

        public async void TurnOn(float waitTime)
        {
            _light.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(waitTime));
            _light.SetActive(false);
        }
    }
}
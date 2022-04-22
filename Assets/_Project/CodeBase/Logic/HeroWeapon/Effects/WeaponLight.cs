using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon.Effects
{
    public class WeaponLight : MonoBehaviour
    {
        [SerializeField] private GameObject _light;

        public async void TurnOn(float waitTime)
        {
            _light.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(waitTime));
            _light.SetActive(false);
        }
    }
}
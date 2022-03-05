using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Project.CodeBase.Logic.Weapon
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private int _poolSize;

        private List<Bullet> _availableBullets = new List<Bullet>();

        private void Start()
        {
            for (var i = 0; i < _availableBullets.Count; i++)
            {
                var bulletGo = Instantiate(_bulletPrefab, transform);
                bulletGo.SetActive(false);
                var bullet = bulletGo.GetComponent<Bullet>();
                _availableBullets.Add(bullet);
            }
        }

        public async void PickFromPool(Vector3 pos, Vector3 velocity)
        {
            if (_availableBullets.Count < 1) return;
            var bullet = _availableBullets[0];
            bullet.Activate(pos, velocity);
            _availableBullets.Remove(bullet);
            await UniTask.Delay((int) bullet.LifeTime);
            AddToPool(bullet);
        }

        public void AddToPool(Bullet bullet)
        {
            if (!_availableBullets.Contains(bullet))
                _availableBullets.Add(bullet);
        }
    }
}
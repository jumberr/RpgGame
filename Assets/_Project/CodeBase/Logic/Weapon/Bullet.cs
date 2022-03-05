using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        //public float shootForce;
        //public float upwardForce;
        //public GameObject bullet;
//
        //public Bullet(float shootForce, float upwardForce, GameObject bullet)
        //{
        //    this.shootForce = shootForce;
        //    this.upwardForce = upwardForce;
        //    this.bullet = bullet;
        //}

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _lifeTime;

        public float LifeTime => _lifeTime;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Bullet hit");
            Deactivate();
        }

        public async void Activate(Vector3 pos, Vector3 velocity)
        {
            transform.position = pos;
            _rigidbody.velocity = velocity;
            gameObject.SetActive(true);
            await Decay();
        }

        private async UniTask Decay()
        {
            await UniTask.Delay((int) _lifeTime);
            Deactivate();
        }

        private void Deactivate() => 
            gameObject.SetActive(false);
    }
}
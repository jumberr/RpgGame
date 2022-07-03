using System;
using UnityEngine;

namespace _Project.CodeBase.Logic
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public event Action HealthChanged;
        public float Current { get; set; }
        public float Max { get; set; }
        public void TakeDamage(float damage)
        {
            Debug.Log(damage);
        }
    }
}
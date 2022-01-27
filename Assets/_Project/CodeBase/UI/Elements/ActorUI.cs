﻿using _Project.CodeBase.Logic;
using UnityEngine;

namespace _Project.CodeBase.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar HpBar;

        private IHealth _heroHealth;

        public void Construct(IHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HealthChanged += UpdateHpBar;
        }

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
        }

        private void OnDestroy() => 
            _heroHealth.HealthChanged -= UpdateHpBar;

        private void UpdateHpBar() => 
            HpBar.SetValue(_heroHealth.Current, _heroHealth.Max);
    }
}
using _Project.CodeBase.Logic.Hero;
using NTC.Global.Cache;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyMovement : NightCache, INightRun
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float maxDistance;
        [SerializeField] private float maxTime;

        private Transform _hero;
        private float _timer;

        public void Initialize(Transform hero) => 
            _hero = hero;

        public void Run()
        {
            _timer -= Time.deltaTime;
            if (_timer < 0f)
            {
                var sqrDistance =  (_hero.position - agent.destination).sqrMagnitude;
                if (sqrDistance > maxDistance * maxDistance) 
                    agent.destination = _hero.position;
                _timer = maxTime;
            }
        }
    }
}
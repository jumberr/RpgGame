using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private GameObject heroPrefab;
        
        public override void InstallBindings()
        {
            //var playerMovement = Container
            //    .InstantiatePrefabForComponent<PlayerMovement>(heroPrefab, startPoint.position, Quaternion.identity, null);
//
            //Container
            //    .Bind<PlayerMovement>()
            //    .FromInstance(playerMovement)
            //    .AsSingle();

            //var hero = Container.InstantiatePrefab(heroPrefab, startPoint.position, Quaternion.identity, null);
            //Container.Bind<PlayerMovement>().FromInstance();
        }
    }
}
using _Project.CodeBase.Hero;
using _Project.CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assetProvider;
        private GameObject HeroGameObject { get; set; }
        private GameObject CameraGameObject { get; set; }

        public GameFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiatePrefab(AssetPath.HeroPath, at.transform.position);
            var playerMovement = HeroGameObject.GetComponent<PlayerMovement>();
            var playerAnimator = HeroGameObject.GetComponent<PlayerAnimator>();
            var camera = HeroGameObject.GetComponentInChildren<PlayerCamera>().transform;
            
            playerMovement.Construct(playerAnimator, camera);
            playerAnimator.Construct(playerMovement);

            return HeroGameObject;
        }

        private GameObject InstantiatePrefab(string prefabPath, Vector3 position) => 
            _assetProvider.Instantiate(prefabPath, position);
    }
}
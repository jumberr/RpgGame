using System.Threading.Tasks;
using _Project.CodeBase.Hero;
using _Project.CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assetProvider;
        private GameObject HeroGameObject { get; set; }

        public GameFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public async Task<GameObject> CreateHero(GameObject at)
        {
            HeroGameObject = await _assetProvider.InstantiateAsync(AssetPath.HeroPath, at.transform.position);
            var playerMovement = HeroGameObject.GetComponent<PlayerMovement>();
            var playerAnimator = HeroGameObject.GetComponent<PlayerAnimator>();
            
            playerMovement.Construct(playerAnimator);

            return HeroGameObject;
        }
    }
}
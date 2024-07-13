using _Project.CodeBase.Logic.Hero.Cam;
using _Project.CodeBase.Logic.Hero.Reload;
using _Project.CodeBase.Logic.Hero.Shooting;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Services;
using _Project.CodeBase.Utils.Factory;
using NTC.Global.Cache;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroFacade : NightCache
    {
        private IUIFactory _uiFactory;
        
        [field: SerializeField] public ActiveItemController ActiveItemController { get; private set; }
        [field: SerializeField] public HeroInteraction Interaction { get; private set; }
        [field: SerializeField] public HeroInventory Inventory { get; private set; }
        [field: SerializeField] public HeroMovement Movement { get; private set; }
        [field: SerializeField] public HeroCamera Camera { get; private set; }
        [field: SerializeField] public HeroAnimator Animator { get; private set; }
        [field: SerializeField] public HeroScoping Scoping { get; private set; }
        [field: SerializeField] public HeroAttack Attack { get; private set; }
        [field: SerializeField] public HeroReload Reload { get; private set; }
        [field: SerializeField] public HeroHealth Health { get; private set; }
        [field: SerializeField] public HeroRecoil Recoil { get; private set; }
        [field: SerializeField] public HeroDeath Death { get; private set; }
        [field: SerializeField] public HeroState State { get; private set; }
        [field: SerializeField] public HeroAmmo Ammo { get; private set; }
        [field: SerializeField] public FallingDamage FallingDamage { get; private set; }
        [field: SerializeField] public PlayerMonoComponentsProvider ComponentsProvider { get; private set; }
        [field: SerializeField] public FootstepsController FootstepsController { get; private set; }
        [field: SerializeField] public Transform CameraTransform { get; private set; }
        [field: SerializeField] public TrajectoryView TrajectoryView { get; private set; }

        
        [Inject]
        private void Construct(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            Death.SetHealthComponent(Health);
            
            Death.ZeroHealth += _uiFactory.ShowDeathScreen;
        }

        private void OnDestroy() => 
            Death.ZeroHealth -= _uiFactory.ShowDeathScreen;

        public class Factory : ComponentPlaceholderFactory<HeroFacade>
        {
        }
    }
}
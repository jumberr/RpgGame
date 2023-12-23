using _Project.CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class TrajectoryDrawer
    {
        private readonly IGameFactory _gameFactory;
        private TrajectoryView _view;


        public TrajectoryDrawer(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _gameFactory.HeroCreated += HeroCreated;
        }

        public void ShowTrajectory(Vector3 origin, Vector3 speed)
        {
            _view.Activate();
            
            var points = new Vector3[_view.DefaultPointsCount];

            for (var i = 0; i < points.Length; i++)
            {
                var time = i * 0.1f;
                points[i] = origin + speed * time + Physics.gravity * time * time / 2f;
            }
            
            _view.SetPositions(points);
        }
        
        public void HideTrajectory() => 
            _view.Deactivate();
        
        private void HeroCreated(HeroFacade heroFacade)
        {
            _gameFactory.HeroCreated -= HeroCreated;
            _view = heroFacade.TrajectoryView;
        }
    }
}
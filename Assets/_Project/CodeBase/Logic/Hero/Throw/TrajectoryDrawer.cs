using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.UI;
using _Project.CodeBase.UI.Services;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class TrajectoryDrawer
    {
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private TrajectoryView _view;
        private ActionProgressView _progressView;


        public TrajectoryDrawer(IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _gameFactory.HeroCreated += HeroCreated;
            _uiFactory.UICreated += UICreated;
        }

        public void ShowTrajectory(Vector3 origin, Vector3 speed, float progress)
        {
            ToggleTrajectory(true);
            
            var points = new Vector3[_view.DefaultPointsCount];

            for (var i = 0; i < points.Length; i++)
            {
                var time = i * 0.1f;
                points[i] = origin + speed * time + Physics.gravity * time * time / 2f;
            }
            
            _view.SetPositions(points);
            _progressView.SetProgress(progress);
        }

        public void ToggleTrajectory(bool value)
        {
            _progressView.ToggleProgressbar(value);
            _view.ToggleTrajectory(value);
        }

        private void HeroCreated(HeroFacade heroFacade)
        {
            _gameFactory.HeroCreated -= HeroCreated;
            _view = heroFacade.TrajectoryView;
        }

        private void UICreated()
        {
            _uiFactory.UICreated -= UICreated;
            _progressView = _uiFactory.ActorUI.ActionProgressView;
        }
    }
}
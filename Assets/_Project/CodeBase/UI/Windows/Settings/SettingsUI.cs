using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Logic.Hero;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows.Settings
{
    public class SettingsUI : WindowBase, ISavedProgress
    {
        public event Action<float> OnUpdateSensitivity;
        
        [SerializeField] private Slider _slider;

        private HeroRotation _rotation;
        private SettingsData _settings;
        
        public void Construct(HeroRotation rotation)
        {
            _rotation = rotation;
            OnUpdateSensitivity += rotation.UpdateSensitivity;
        }
        
        public void LoadProgress(PlayerProgress progress)
        {
            _settings = progress.Settings;
            _slider.value = _settings.Sensitivity;
            ChangeValue(_settings.Sensitivity);
        }

        public void UpdateProgress(PlayerProgress progress) => 
            progress.Settings = _settings;

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            _slider.onValueChanged.AddListener(ChangeValue);
        }

        protected override void Cleanup()
        {
            OnUpdateSensitivity -= _rotation.UpdateSensitivity;
            _slider.onValueChanged.RemoveAllListeners();
        }

        private void ChangeValue(float value)
        {
            _settings.Sensitivity = value;
            OnUpdateSensitivity?.Invoke(_settings.Sensitivity);
        }
    }
}
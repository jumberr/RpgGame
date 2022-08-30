﻿using System;
using System.Collections.Generic;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Logic.Hero;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows.Settings
{
    public class SettingsUI : WindowBase, ISavedProgress
    {
        public event Action<float> OnUpdateSensitivity;
        
        [SerializeField] private Slider _sensitivitySlider;
        [SerializeField] private TMP_Dropdown _qualityDropdown;

        private HeroRotation _rotation;
        private SettingsData _settings;
        
        public void Construct(HeroRotation rotation)
        {
            _rotation = rotation;
            OnUpdateSensitivity += rotation.UpdateSensitivity;
        }

        private void Start()
        {
            _qualityDropdown.options = new List<TMP_Dropdown.OptionData>();
            foreach (var quality in QualitySettings.names)
            {
                _qualityDropdown.options.Add(new TMP_Dropdown.OptionData(quality));
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _settings = progress.Settings;
            _sensitivitySlider.value = _settings.Sensitivity;
            _qualityDropdown.value = _settings.Quality;
            ChangeSensitivityValue(_settings.Sensitivity);
            ChangeQualitySettings(_settings.Quality);
        }

        public void UpdateProgress(PlayerProgress progress) => 
            progress.Settings = _settings;

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            _sensitivitySlider.onValueChanged.AddListener(ChangeSensitivityValue);
            _qualityDropdown.onValueChanged.AddListener(ChangeQualitySettings);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            OnUpdateSensitivity -= _rotation.UpdateSensitivity;
            _sensitivitySlider.onValueChanged.RemoveListener(ChangeSensitivityValue);
            _qualityDropdown.onValueChanged.RemoveListener(ChangeQualitySettings);
        }

        private void ChangeSensitivityValue(float value)
        {
            _settings.Sensitivity = value;
            OnUpdateSensitivity?.Invoke(_settings.Sensitivity);
        }
        
        private void ChangeQualitySettings(int quality)
        {
            _settings.Quality = quality;
            QualitySettings.SetQualityLevel(quality, true);
        }
    }
}
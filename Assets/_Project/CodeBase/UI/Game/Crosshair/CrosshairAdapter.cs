﻿using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.UI.MVA;
using NTC.Global.Cache;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public class CrosshairAdapter : NightCache, INightRun, IAdapter
    {
        [SerializeField] private CrosshairView _view;

        [SerializeField] private float _restingSize;
        [SerializeField] private float _maxSize;
        [SerializeField] private float _speed;

        private InputService _inputService;
        private WeaponController _weaponController;
        private HeroState _heroState;
        private CrosshairState _state;

        private Vector2 _prevInput;
        private Vector2 _input;
        private float _currentSize;

        public void Construct(WeaponController weaponController, InputService inputService, HeroState heroState)
        {
            _inputService = inputService;
            _weaponController = weaponController;
            _heroState = heroState;
            Subscribe();
            Switch(false);
            HideAndShow();
        }

        public void Run()
        {
            if (_state != CrosshairState.Cross) return;

            _currentSize = Mathf.Lerp(_currentSize, _input.magnitude > _prevInput.magnitude 
                ? _maxSize 
                : _restingSize, Time.deltaTime * _speed);
            
            _view.ApplySize(_currentSize);
        }

        private void OnDisable() => 
            UnSubscribe();

        private void Subscribe()
        {
            _weaponController.OnSwitch += Switch;
            _inputService.MoveAction.Event += OnMove;
            _heroState.OnAimingChanged += AimingChange;
        }

        private void UnSubscribe()
        {
            _weaponController.OnSwitch -= Switch;
            _inputService.MoveAction.Event -= OnMove;
            _heroState.OnAimingChanged -= AimingChange;
        }

        private void Switch(bool isWeapon)
        {
            _state = isWeapon ? CrosshairState.Cross : CrosshairState.Dot;
            HideAndShow();
        }

        private void OnMove(Vector2 dir)
        {
            _prevInput = _input;
            _input = dir;
        }

        private void AimingChange()
        {
            if (_heroState.Aiming)
                _view.Hide();
            else
                _view.Show(_state);
        }

        private void HideAndShow()
        {
            _view.Hide();
            _view.Show(_state);
        }
    }
}
﻿using System.Collections.Generic;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData.ItemsDataBase;
using _Project.CodeBase.UI.Animation;
using _Project.CodeBase.Utils.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows.Inventory
{
    public class ItemDescription : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private WeaponStatsUI _weaponStatsUI;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [Space]
        [SerializeField] private SimpleScaleUIAnimation _scaleAnimation;

        private List<Component> _components;
        private bool _hidden;

        private void Start()
        {
            InitializeComponents();
            Hide();
        }

        public void UpdateView(ItemData data)
        {
            Show();
            Animate();
            UpdateWeaponPart(data);
            UpdateGeneralPart(data);
        }

        private void InitializeComponents() => 
            _components = new List<Component> {_background, _icon, _name, _description};

        private void Animate() => 
            _scaleAnimation.Scale();

        private void UpdateWeaponPart(ItemData data)
        {
            if (data.ItemPayloadData.ItemType == ItemType.Weapon)
            {
                _weaponStatsUI.gameObject.SetActive(true);
                _weaponStatsUI.UpdateView((Weapon) data);
            }
            else
                _weaponStatsUI.gameObject.SetActive(false);
        }

        private void UpdateGeneralPart(ItemData data)
        {
            _icon.sprite = data.ItemUIData.Icon;
            _name.text = data.ItemUIData.Name.ToString();
            _description.text = data.ItemUIData.Description;
        }

        private void Show()
        {
            if (!_hidden)
                return;
            _components.ActivateComponents();
            _hidden = false;
        }

        private void Hide()
        {
            _hidden = true;
            _components.DeactivateComponents();
        }
    }
}
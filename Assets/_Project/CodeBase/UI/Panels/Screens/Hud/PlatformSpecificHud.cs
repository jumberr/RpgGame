using System.Collections.Generic;
using _Project.CodeBase.Utils.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public class PlatformSpecificHud : MonoBehaviour
    {
        [SerializeField] private bool _debugMode;
        [SerializeField, ShowIf("_debugMode")] private RuntimePlatform _platform;
        [Space] 
        [SerializeField] private List<GameObject> _mobileButtons;

        public void Initialize() => 
            InitializeByPlatform(_debugMode ? _platform : Application.platform);

        private void InitializeByPlatform(RuntimePlatform platform)
        {
            switch (platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    InitializeWindowsHud();
                    break;
                case RuntimePlatform.Android:
                case RuntimePlatform.IPhonePlayer:
                    InitializeMobileHud();
                    break;
            }
        }

        private void InitializeWindowsHud()
        {
            _mobileButtons.DeactivateComponents();
        }

        private void InitializeMobileHud()
        {
            _mobileButtons.ActivateComponents();
        }
    }
}
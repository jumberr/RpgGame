using System;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class SettingsData
    {
        public SettingsData(float sensitivity)
        {
            Sensitivity = sensitivity;
        }

        public float Sensitivity;
    }
}
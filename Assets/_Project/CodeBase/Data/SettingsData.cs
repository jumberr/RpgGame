using System;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class SettingsData
    {
        public SettingsData(float sensitivity, int quality)
        {
            Sensitivity = sensitivity;
            Quality = quality;
        }

        public float Sensitivity;
        public int Quality;
    }
}
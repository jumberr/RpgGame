using System;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class SettingsData
    {
        public float Sensitivity;
        public int Quality;
        
        public SettingsData(float sensitivity, int quality)
        {
            Sensitivity = sensitivity;
            Quality = quality;
        }
    }
}
using _Project.CodeBase.Infrastructure;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "ProjectSettings", menuName = "Static Data/Project Settings", order = 0)]
    public class ProjectSettings : ScriptableObject
    {
        public LoadingCurtain LoadingCurtain;
        public string InitialScene;
        public string MenuScene;
        public string GameScene;
    }
}
using _Project.CodeBase.Infrastructure;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "ProjectSettings", menuName = "Scriptable Objects/Project Settings", order = 0)]
    public class ProjectSettings : ScriptableObject
    {
        public LoadingCurtain LoadingCurtain;
        public string InitialScene;
        public string GameScene;
    }
}
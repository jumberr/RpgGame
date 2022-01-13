using UnityEngine;

namespace _Project.CodeBase.Infrastructure
{
    [CreateAssetMenu(fileName = "ProjectSettings", menuName = "Scriptable Objects/Project Settings", order = 0)]
    public class ProjectSettings : ScriptableObject
    {
        [SerializeField] private LoadingCurtain loadingCurtain;

        public LoadingCurtain LoadingCurtain => loadingCurtain;
    }
}
using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace _Project.CodeBase.StaticData.Sound
{
    [CreateAssetMenu(fileName = nameof(SurfaceSoundConfig), menuName = "Static Data/Sound/" + nameof(SurfaceSoundConfig), order = 0)]
    public class SurfaceSoundConfig : ScriptableObject
    {
        [SerializeField] private SurfaceSoundData _surfaceData;
        
        
        public List<AudioData> GetSounds(SurfaceType surfaceType, MoveType moveType) => 
            _surfaceData[surfaceType][moveType];
        
        public MoveSoundData GetMoveConfig(SurfaceType surfaceType) => 
            _surfaceData[surfaceType];
    }
}
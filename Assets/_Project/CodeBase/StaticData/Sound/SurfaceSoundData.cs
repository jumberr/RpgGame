using System;
using _Project.CodeBase.Infrastructure.Services;
using _Project.CodeBase.Utils;

namespace _Project.CodeBase.StaticData.Sound
{
    [Serializable]
    public class SurfaceSoundData : SerializableDictionary<SurfaceType, MoveSoundData>
    {
    }
}
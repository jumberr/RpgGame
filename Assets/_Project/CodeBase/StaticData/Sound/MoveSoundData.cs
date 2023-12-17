using System;
using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services;
using _Project.CodeBase.Utils;

namespace _Project.CodeBase.StaticData.Sound
{
    [Serializable]
    public class MoveSoundData : SerializableDictionary<MoveType, List<AudioData>>
    {
    }
}
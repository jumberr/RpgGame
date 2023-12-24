using System;
using _Project.CodeBase.Logic.Hero;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class ThrowableInfo : ItemInfo
    {
        [field: SerializeField] public ThrowData ThrowData { get; private set; }
    }
}
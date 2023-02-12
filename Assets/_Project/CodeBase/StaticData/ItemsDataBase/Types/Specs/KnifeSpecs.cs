using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class KnifeSpecs
    {
        [SerializeField] private float _radius;
        
        public float Radius => _radius;
    }
}
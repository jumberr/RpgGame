using System;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public struct RotationData
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public RotationData(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}
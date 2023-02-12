using System;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public struct PositionData
    {
        public float X;
        public float Y;
        public float Z;

        public PositionData(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
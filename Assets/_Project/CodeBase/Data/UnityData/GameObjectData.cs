using System;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public struct GameObjectData
    {
        public PositionData Position;
        public RotationData Rotation;

        public GameObjectData(PositionData position, RotationData rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public void Update(PositionData position, RotationData rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}
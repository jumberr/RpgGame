using UnityEngine;

namespace _Project.CodeBase.Data
{
    public static class DataExtensions
    {
        public static PositionData AsVectorData(this Vector3 vector3) =>
            new PositionData(vector3.x, vector3.y, vector3.z);

        public static Vector3 AsUnityVector(this PositionData vector3Data) =>
            new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);

        public static Vector3 AddY(this Vector3 vector3, float y)
        {
            vector3.y += y;
            return vector3;
        }
    }
}
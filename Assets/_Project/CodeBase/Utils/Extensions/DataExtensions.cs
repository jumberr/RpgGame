using _Project.CodeBase.Data;
using UnityEngine;

namespace _Project.CodeBase.Utils.Extensions
{
    public static class DataExtensions
    {
        public static PositionData AsVectorData(this Vector3 vector3) =>
            new PositionData(vector3.x, vector3.y, vector3.z);

        public static Vector3 AsUnityVector(this PositionData vector3Data) =>
            new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);

        public static RotationData AsRotationData(this Quaternion quaternion) => 
            new RotationData(quaternion.x, quaternion.y, quaternion.z, quaternion.w);

        public static Quaternion AsQuaternion(this RotationData quaternion) => 
            new Quaternion(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

        public static Vector3 AddY(this Vector3 vector3, float y)
        {
            vector3.y += y;
            return vector3;
        }
    }
}
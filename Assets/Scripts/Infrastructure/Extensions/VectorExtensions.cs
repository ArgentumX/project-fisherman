using UnityEngine;

namespace Infrastructure.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 ToUnityVector3(this System.Numerics.Vector3 source)
        {
            return new Vector3(source.X, source.Y, source.Z);
        }

        public static System.Numerics.Vector3 ToSystemVector3(this Vector3 source)
        {
            return new System.Numerics.Vector3(source.x, source.y, source.z);
        }
    }
}
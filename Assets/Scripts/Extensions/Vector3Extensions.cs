using UnityEngine;

namespace EcsLiteTestProject
{
    public static class Vector3Extensions
    {
        public static Vector3 SetX(this Vector3 vector, float x)
        {
            Vector3 tempVector = vector;
            tempVector.x = x;
            vector = tempVector;

            return vector;
        }

        public static Vector3 SetY(this Vector3 vector, float y)
        {
            Vector3 tempVector = vector;
            tempVector.y = y;
            vector = tempVector;

            return vector;
        }

        public static Vector3 SetZ(this Vector3 vector, float z)
        {
            Vector3 tempVector = vector;
            tempVector.z = z;
            vector = tempVector;

            return vector;
        }
    }
}
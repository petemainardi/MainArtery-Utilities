using UnityEngine;

namespace MainArtery.Utilities.Unity
{
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    /**
    *  Extension methods for the Unity Vector3 and Vector3Int classes.
    */
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    public static class Vector3Extensions
    {
        public static Vector3 WithX(this Vector3 v, int x) => new Vector3(x, v.y, v.z);
        public static Vector3 WithY(this Vector3 v, int y) => new Vector3(v.x, y, v.z);
        public static Vector3 WithZ(this Vector3 v, int z) => new Vector3(v.x, v.y, z);
        public static Vector3 WithXY(this Vector3 v, int x, int y) => new Vector3(x, y, v.z);
        public static Vector3 WithXZ(this Vector3 v, int x, int z) => new Vector3(x, v.y, z);
        public static Vector3 WithYZ(this Vector3 v, int y, int z) => new Vector3(v.x, y, z);

        public static Vector3Int WithX(this Vector3Int v, int x) => new Vector3Int(x, v.y, v.z);
        public static Vector3Int WithY(this Vector3Int v, int y) => new Vector3Int(v.x, y, v.z);
        public static Vector3Int WithZ(this Vector3Int v, int z) => new Vector3Int(v.x, v.y, z);
        public static Vector3Int WithXY(this Vector3Int v, int x, int y) => new Vector3Int(x, y, v.z);
        public static Vector3Int WithXZ(this Vector3Int v, int x, int z) => new Vector3Int(x, v.y, z);
        public static Vector3Int WithYZ(this Vector3Int v, int y, int z) => new Vector3Int(v.x, y, z);
    }
}

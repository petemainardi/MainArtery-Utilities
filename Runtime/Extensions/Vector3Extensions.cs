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
        public static Vector3 WithX(this Vector3 v, float x) => new Vector3(x, v.y, v.z);
        public static Vector3 WithY(this Vector3 v, float y) => new Vector3(v.x, y, v.z);
        public static Vector3 WithZ(this Vector3 v, float z) => new Vector3(v.x, v.y, z);
        public static Vector3 WithXY(this Vector3 v, float x, float y) => new Vector3(x, y, v.z);
        public static Vector3 WithXZ(this Vector3 v, float x, float z) => new Vector3(x, v.y, z);
        public static Vector3 WithYZ(this Vector3 v, float y, float z) => new Vector3(v.x, y, z);
        public static Vector3 WithX(this Vector3 v, Vector3 x) => new Vector3(x.x, v.y, v.z);
        public static Vector3 WithY(this Vector3 v, Vector3 y) => new Vector3(v.x, y.y, v.z);
        public static Vector3 WithZ(this Vector3 v, Vector3 z) => new Vector3(v.x, v.y, z.z);
        public static Vector3 WithXY(this Vector3 v, Vector3 xy) => new Vector3(xy.x, xy.y, v.z);
        public static Vector3 WithXZ(this Vector3 v, Vector3 xz) => new Vector3(xz.x, v.y, xz.z);
        public static Vector3 WithYZ(this Vector3 v, Vector3 yz) => new Vector3(v.x, yz.y, yz.z);

        public static Vector3Int WithX(this Vector3Int v, int x) => new Vector3Int(x, v.y, v.z);
        public static Vector3Int WithY(this Vector3Int v, int y) => new Vector3Int(v.x, y, v.z);
        public static Vector3Int WithZ(this Vector3Int v, int z) => new Vector3Int(v.x, v.y, z);
        public static Vector3Int WithXY(this Vector3Int v, int x, int y) => new Vector3Int(x, y, v.z);
        public static Vector3Int WithXZ(this Vector3Int v, int x, int z) => new Vector3Int(x, v.y, z);
        public static Vector3Int WithYZ(this Vector3Int v, int y, int z) => new Vector3Int(v.x, y, z);
        public static Vector3Int WithX(this Vector3Int v, Vector3Int x) => new Vector3Int(x.x, v.y, v.z);
        public static Vector3Int WithY(this Vector3Int v, Vector3Int y) => new Vector3Int(v.x, y.y, v.z);
        public static Vector3Int WithZ(this Vector3Int v, Vector3Int z) => new Vector3Int(v.x, v.y, z.z);
        public static Vector3Int WithXY(this Vector3Int v, Vector3Int xy) => new Vector3Int(xy.x, xy.y, v.z);
        public static Vector3Int WithXZ(this Vector3Int v, Vector3Int xz) => new Vector3Int(xz.x, v.y, xz.z);
        public static Vector3Int WithYZ(this Vector3Int v, Vector3Int yz) => new Vector3Int(v.x, yz.y, yz.z);
    }
}

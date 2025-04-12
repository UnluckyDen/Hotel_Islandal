using UnityEngine;

namespace _Main.Scripts.Utils
{
    public static class UnluckyExtensions
    {
        public static Vector3Int ToVector3Int(this Vector3 vector3) =>
            new(Mathf.RoundToInt(vector3.x),
                Mathf.RoundToInt(vector3.y),
                Mathf.RoundToInt(vector3.z));
    }
}
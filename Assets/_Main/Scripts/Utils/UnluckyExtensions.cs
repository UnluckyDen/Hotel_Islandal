using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.Utils
{
    public static class UnluckyExtensions
    {
        public static Vector3Int ToVector3Int(this Vector3 vector3) =>
            new(Mathf.RoundToInt(vector3.x),
                Mathf.RoundToInt(vector3.y),
                Mathf.RoundToInt(vector3.z));

        public static T RandomElementFromList<T>(this List<T> listToRandom) =>
            listToRandom[Random.Range(0, listToRandom.Count)];

        public static void RandomShuffleListElements<T>(this List<T> listToRandom)
        {
            for (int i = 0; i < listToRandom.Count; i++)
            {
                T temp = listToRandom[i];
                int randomIndex = Random.Range(i, listToRandom.Count);
                listToRandom[i] = listToRandom[randomIndex];
                listToRandom[randomIndex] = temp;
            }
        }
    }
}
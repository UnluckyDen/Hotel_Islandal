using UnityEngine;

namespace _Main.Scripts.Environment.Systems.Temperature
{
    public class TemperaturePoint : MonoBehaviour
    {
        [SerializeField] private float _temperature;

        public float Temperature => _temperature;
    }
}
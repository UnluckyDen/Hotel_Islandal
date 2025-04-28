using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.Environment.Systems.Temperature
{
    public class TemperatureController : MonoBehaviour
    {
        [SerializeField] private List<TemperaturePoint> _temperaturePoints;
        [SerializeField] private float _mainTemperature;

        public float GetMainTemperature()
        {
            return _mainTemperature;
        }
    }
}
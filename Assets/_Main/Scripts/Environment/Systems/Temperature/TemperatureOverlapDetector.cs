using System;
using System.Collections.Generic;
using _Main.Scripts.Utils.OverlapDetectors;
using UnityEngine;

namespace _Main.Scripts.Environment.Systems.Temperature
{
    public class TemperatureOverlapDetector : MonoBehaviour
    {
        [SerializeField] private OverlapDetector _overlapDetector;

        private TemperatureController _temperatureController;

        public void Init(TemperatureController temperatureController)
        {
            _temperatureController = temperatureController;
        }

        public float GetCurrentTemperature()
        {
            if (!_overlapDetector.Check())
                return _temperatureController.GetMainTemperature();

            List<TemperaturePoint> temperaturePoints = new List<TemperaturePoint>();
            float currentTemperature = _temperatureController.GetMainTemperature();
            float resultMinDistance = float.MaxValue;

            for (int i = 0; i < _overlapDetector.ResultCount; i++)
            {
                Collider collider = _overlapDetector.ResultColliders[i];
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                if (distance > resultMinDistance)
                    continue;

                var tempTemperaturePoint = collider.GetComponent<TemperaturePoint>();
                if (tempTemperaturePoint != null)
                {
                    temperaturePoints.Add(tempTemperaturePoint);
                    resultMinDistance = distance;
                }
            }

            return currentTemperature;
        }
    }
}
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
                return _temperatureController.GetRoomAveragePoint().Temperature;

            List<TemperaturePoint> temperaturePoints = new List<TemperaturePoint>();

            for (int i = 0; i < _overlapDetector.ResultCount; i++)
            {
                Collider collider = _overlapDetector.ResultColliders[i];
                var tempTemperaturePoint = collider.GetComponent<TemperaturePoint>();
                
                if (tempTemperaturePoint != null)
                    temperaturePoints.Add(tempTemperaturePoint);
            }

            temperaturePoints.Add(_temperatureController.GetRoomAveragePoint());
            return CalculateWeightedAverageTemperature(temperaturePoints); 
        }
        
        public float CalculateWeightedAverageTemperature(List<TemperaturePoint> points)
        {
            float sumWeightedTemperatures = 0f;
            float sumWeights = 0f;

            foreach (var point in points)
            {
                if (point == null) continue;

                float distance = Vector3.Distance(transform.position, point.transform.position);
            
                if (distance < 0.0001f) 
                    distance = 0.0001f;
            
                float weight = 1f / distance;
            
                sumWeightedTemperatures += point.Temperature * weight;
                sumWeights += weight;
            }
            
            float averageTemperature = sumWeightedTemperatures / sumWeights;

            return averageTemperature;
        }
    }
}
using System.Collections.Generic;
using _Main.Scripts.Environment.Systems.Temperature;
using _Main.Scripts.Utils.OverlapDetectors;
using UnityEngine;

namespace _Main.Scripts.Environment.Systems.Radiation
{
    public class RadiationOverlapDetector : MonoBehaviour
    {
        [SerializeField] private OverlapDetector _overlapDetector;

        private RadiationController _radiationController;

        public void Init(RadiationController radiationController)
        {
            _radiationController = radiationController;
        }

        public float GetCurrentRadiation()
        {
            if (!_overlapDetector.Check())
                return _radiationController.GetRoomAveragePoint().Radiation;

            List<RadiationPoint> radiationPoints = new List<RadiationPoint>();

            for (int i = 0; i < _overlapDetector.ResultCount; i++)
            {
                Collider collider = _overlapDetector.ResultColliders[i];
                var tempRadiationPoint = collider.GetComponent<RadiationPoint>();
                
                if (tempRadiationPoint != null)
                    radiationPoints.Add(tempRadiationPoint);
            }

            radiationPoints.Add(_radiationController.GetRoomAveragePoint());
            return CalculateWeightedAverageRadiation(radiationPoints); 
        }
        
        public float CalculateWeightedAverageRadiation(List<RadiationPoint> points)
        {
            float sumWeightedRadiation = 0f;
            float sumWeights = 0f;

            foreach (var point in points)
            {
                if (point == null) continue;

                float distance = Vector3.Distance(transform.position, point.transform.position);
            
                if (distance < 0.0001f) 
                    distance = 0.0001f;
            
                float weight = 1f / distance;
            
                sumWeightedRadiation += point.Radiation * weight;
                sumWeights += weight;
            }
            
            float averageRadiation = sumWeightedRadiation / sumWeights;

            return averageRadiation;
        }
    }
}
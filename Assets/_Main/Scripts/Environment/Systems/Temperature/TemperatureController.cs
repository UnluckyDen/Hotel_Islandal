using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Main.Scripts.Environment.Systems.Temperature
{
    public class TemperatureController : MonoBehaviour
    {
        [SerializeField] private List<TemperaturePoint> _roomAveragePoints;

        public TemperaturePoint GetRoomAveragePoint()
        {
            return _roomAveragePoints.FirstOrDefault();
        }
    }
}
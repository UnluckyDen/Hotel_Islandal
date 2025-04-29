using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Main.Scripts.Environment.Systems.Radiation
{
    public class RadiationController : MonoBehaviour
    {
        [SerializeField] private List<RadiationPoint> _roomAveragePoints;

        public RadiationPoint GetRoomAveragePoint()
        {
            return _roomAveragePoints.FirstOrDefault();
        }
    }
}
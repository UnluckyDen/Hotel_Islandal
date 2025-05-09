using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.Player.Movement.Way
{
    public class WayPoint : MonoBehaviour
    {
        private readonly Dictionary<Vector3Int, WayPoint> _wayPoints = new();

        public void SetNeighboringCell(Vector3Int direction, WayPoint wayPoint) => 
            _wayPoints.Add(direction, wayPoint);

        public void SetNeighboringCell(KeyValuePair<Vector3Int, WayPoint> directionWaypointPair) =>
            _wayPoints.Add(directionWaypointPair.Key, directionWaypointPair.Value);

        public void SetNeighboringCell(List<KeyValuePair<Vector3Int, WayPoint>> directionWaypointPairs)
        {
            foreach (var pair in directionWaypointPairs)
                _wayPoints.Add(pair.Key,pair.Value);
        }

        public WayPoint GetNextWayPoint(Vector3Int direction) => 
            _wayPoints.GetValueOrDefault(direction);

        private void OnDrawGizmosSelected()
        {
            if (_wayPoints.Count <= 0) 
                return;
            
            foreach (var wayPointPair in _wayPoints)
                Debug.DrawRay(transform.position, wayPointPair.Value.transform.position - transform.position, Color.green);
        }
    }
}
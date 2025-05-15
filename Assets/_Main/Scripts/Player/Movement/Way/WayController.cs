using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.Utils;
using UnityEngine;

namespace _Main.Scripts.Player.Movement.Way
{
    public class WayController : MonoBehaviour
    {
        [SerializeField] private float _pointsLinkDistance = 1f;
        [SerializeField] private List<WayPoint> _wayPoints;

        private WayPoint _currentWayPoint;

        public WayPoint CurrentWayPoint => _currentWayPoint;

        public WayPoint GetNextWayPoint(Vector3Int direction)
        {
            var nextWayPoint = _currentWayPoint.GetNextWayPoint(direction);
            if (nextWayPoint == null) 
                return null;
            
            return nextWayPoint;
        }

        public void UpdateCurrentWayPoint(WayPoint currentWayPoint)
        {
            _currentWayPoint = currentWayPoint;
        }

        public void CollectWayPoints(List<WayPoint> wayPoints, WayPoint startPoint)
        {
            _currentWayPoint = startPoint;
            _wayPoints.Clear();
            _wayPoints.Add(startPoint);
            _wayPoints.AddRange(wayPoints);
            _wayPoints = 
                _wayPoints.OrderBy(wp => 
                Vector3.Distance(wp.transform.position, startPoint.transform.position))
                .ToList();
        }

        public void LinkWayPoints()
        {
            foreach (var connectingWayPoint in _wayPoints)
            {
                foreach (var connectableWayPoint in _wayPoints)
                {
                    if (connectingWayPoint == connectableWayPoint)
                        continue;
                    
                    if (Vector3.Distance(connectingWayPoint.transform.position, connectableWayPoint.transform.position) <= _pointsLinkDistance)
                    {
                        Vector3Int direction = (connectableWayPoint.transform.position - connectingWayPoint.transform.position).normalized.ToVector3Int();
                        connectingWayPoint.SetNeighboringCell(
                            direction,
                            connectableWayPoint);
                    }
                }
            }
        }

        public void DestroyCurrentPaths()
        {
            if (_currentWayPoint == null)
                return;
            
            _currentWayPoint.Clear();
            _wayPoints.Clear();
        }
    }
}
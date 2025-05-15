using System.Collections.Generic;
using System.Linq;
using System.Text;
using _Main.Scripts.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Main.Scripts.Player.Movement.Way
{
    public class WayController : MonoBehaviour
    {
        [SerializeField] private float _pointsLinkDistance = 1f;
        [SerializeField] private WayPoint _startPoint;
        [SerializeField] private List<WayPoint> _wayPoints;

        private WayPoint _currentWayPoint;

        public WayPoint CurrentWayPoint => _currentWayPoint;

        public void Init()
        {
            _currentWayPoint = _startPoint;
        }

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

        [ContextMenu("CollectWayPointsAtScene")]
        public void CollectWayPointsAtScene()
        {
            
            _wayPoints.Clear();
            _wayPoints.AddRange(Object.FindObjectsByType<WayPoint>(FindObjectsSortMode.None));
            _wayPoints = 
                _wayPoints.OrderBy(wp => 
                Vector3.Distance(wp.transform.position, _startPoint.transform.position))
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
    }
}
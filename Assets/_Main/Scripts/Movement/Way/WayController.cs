using System.Collections.Generic;
using System.Linq;
using System.Text;
using _Main.Scripts.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Main.Scripts.Movement.Way
{
    public class WayController : MonoBehaviour
    {
        [SerializeField] private float _pointsLinkDistance = 1f;
        [SerializeField] private WayPoint _startPoint;
        [SerializeField] private List<WayPoint> _wayPoints;

        private WayPoint _currentWayPoint;

        public WayPoint CurrentWayPoint => _currentWayPoint;

        private void Awake()
        {
            LinkWayPoints();
            _currentWayPoint = _startPoint;
        }

        public WayPoint GetNextWayPoint(Vector3Int direction)
        {
            var nextWayPoint = _currentWayPoint.GetNextWayPoint(direction);
            if (nextWayPoint == null) 
                return null;
            
            _currentWayPoint = nextWayPoint;
            return _currentWayPoint;

        }

        [ContextMenu("CollectWayPointsAtScene")]
        private void CollectWayPointsAtScene()
        {
            
            _wayPoints.Clear();
            _wayPoints.AddRange(Object.FindObjectsByType<WayPoint>(FindObjectsSortMode.None));
            _wayPoints = 
                _wayPoints.OrderBy(wp => 
                Vector3.Distance(wp.transform.position, _startPoint.transform.position))
                .ToList();
        }

        private void LinkWayPoints()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Linked path \n");
            
            foreach (var connectingWayPoint in _wayPoints)
            {
                foreach (var connectableWayPoint in _wayPoints)
                {
                    if (connectingWayPoint == connectableWayPoint)
                        continue;
                    
                    if (Vector3.Distance(connectingWayPoint.transform.position, connectableWayPoint.transform.position) <= _pointsLinkDistance)
                    {
                        Vector3Int direction = (connectableWayPoint.transform.position - connectingWayPoint.transform.position).ToVector3Int();
                        connectingWayPoint.SetNeighboringCell(
                            direction,
                            connectableWayPoint);
                        
                        stringBuilder.Append($"WP1: {connectingWayPoint.name} WP2: {connectableWayPoint.name} connected by direction: {direction} \n");
                    }
                }
            }
            
            Debug.Log(stringBuilder);
        }
    }
}
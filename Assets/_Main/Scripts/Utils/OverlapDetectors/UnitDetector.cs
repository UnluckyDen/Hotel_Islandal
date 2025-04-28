// using UnityEngine;
//
// namespace _Main.Scripts.Utils.OverlapDetectors
// {
//     public class UnitDetector : MonoBehaviour
//     {
//         [SerializeField] private OverlapDetector _overlapDetector;
//
//         public float Distance => _overlapDetector.Size.x;
//
//         private UnitSide _unitSide;
//
//         public void Init(UnitSide unitSide)
//         {
//             _unitSide = unitSide;
//         }
//
//         public IBaseUnit FindNearestUnit()
//         {
//             if (!_overlapDetector.Check())
//                 return null;
//             
//             IBaseUnit resultUnit = null;
//             float resultMinDistance = float.MaxValue;
//             
//             for (int i = 0; i < _overlapDetector.ResultCount; i++)
//             {
//                 Collider collider = _overlapDetector.ResultColliders[i];
//                 float distance = Vector3.Distance(transform.position, collider.transform.position);
//
//                 if (distance > resultMinDistance)
//                     continue;
//
//                 var tempUnit = collider.GetComponent<IBaseUnit>();
//                 if (tempUnit != null && tempUnit.IsAlive && tempUnit.UnitSide != _unitSide)
//                 {
//                     resultUnit = tempUnit;
//                     resultMinDistance = distance;
//                 }
//             }
//
//             return resultUnit;
//         }
//     }
// }
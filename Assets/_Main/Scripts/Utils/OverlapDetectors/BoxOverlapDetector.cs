using UnityEngine;

namespace _Main.Scripts.Utils.OverlapDetectors
{
    public class BoxOverlapDetector : OverlapDetector
    {
        public override bool Check(LayerMask layerMask)
        {
            _resultCount = Physics.OverlapBoxNonAlloc(transform.position, transform.localScale * 0.5f, _resultColliders, transform.rotation, layerMask);
            return _resultCount > 0;
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
#endif
    }
}
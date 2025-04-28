using UnityEngine;

namespace _Main.Scripts.Utils.OverlapDetectors
{
    public class SphereOverlapDetector : OverlapDetector
    {
        public override bool Check(LayerMask layerMask)
        {
            _resultCount = Physics.OverlapSphereNonAlloc(transform.position, transform.localScale.x, _resultColliders, layerMask);
            return _resultCount > 0;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireSphere(transform.position, transform.localScale.x);
        }
#endif
    }
}
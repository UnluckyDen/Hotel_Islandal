using UnityEngine;

namespace _Main.Scripts.Utils.OverlapDetectors
{
    public class RaycastOverlapDetector : OverlapDetector
    {
        private RaycastHit[] _resultRaycastHits;

        protected override void Awake()
        {
            base.Awake();
            _resultRaycastHits = new RaycastHit[_resultMax];
        }

        public override bool Check(LayerMask layerMask)
        {
            _resultCount = Physics.RaycastNonAlloc(transform.position,transform.InverseTransformDirection(Vector3.forward), _resultRaycastHits, transform.localScale.x, layerMask);

            for (int i = 0; i < _resultCount; i++) 
                _resultColliders[i] = _resultRaycastHits[i].collider;

            return _resultCount > 0;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawLine(transform.position, transform.position + transform.InverseTransformDirection(Vector3.forward) * transform.localScale.x);
        }
#endif
    }
}
using UnityEngine;

namespace _Main.Scripts.Utils.OverlapDetectors
{
    public class OverlapDetector : MonoBehaviour
    {
        [SerializeField] protected LayerMask _layerMask;
        [SerializeField] protected int _resultMax = 1;
        [SerializeField] protected Color _gizmoColor = Color.black;
        
        protected int _resultCount;
        protected Collider[] _resultColliders;

        public Vector3 Size => transform.localScale;
        public int ResultCount => _resultCount;
        public Collider[] ResultColliders => _resultColliders;

        protected virtual void Awake() => 
            _resultColliders = new Collider[_resultMax];

        public virtual bool Check() => 
            Check(_layerMask);

        public virtual bool Check(LayerMask layerMask) => false;
    }
}
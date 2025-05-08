using UnityEngine;

namespace _Main.Scripts.Utils.TransformationModifiers
{
    public class AddRandomScaleTransformModifier : BaseTransformModifier
    {
        [SerializeField] private Vector3 _scaleOffsetBound;

        public override void ApplyModifier()
        {
            var newScale = new Vector3(
                transform.localScale.x + Random.Range(-_scaleOffsetBound.x, _scaleOffsetBound.x),
                transform.localScale.y + Random.Range(-_scaleOffsetBound.y, _scaleOffsetBound.y),
                transform.localScale.z + Random.Range(-_scaleOffsetBound.z, _scaleOffsetBound.z));

            transform.localScale = newScale;
        }
    }
}
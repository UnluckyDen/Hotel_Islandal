using UnityEngine;

namespace _Main.Scripts.Utils.TransformationModifiers
{
    public class AddRandomRotateTransformModifier : BaseTransformModifier
    {
        [SerializeField] private bool _asLocal = true;
        [SerializeField] private Vector3 _rotationOffsetBound;

        public override void ApplyModifier()
        {
            Vector3 newAngle;

            if (_asLocal)
            {
                newAngle = new Vector3(
                    transform.localEulerAngles.x + Random.Range(-_rotationOffsetBound.x, _rotationOffsetBound.x),
                    transform.localEulerAngles.y + Random.Range(-_rotationOffsetBound.y, _rotationOffsetBound.y),
                    transform.localEulerAngles.z + Random.Range(-_rotationOffsetBound.z, _rotationOffsetBound.z));
                
                transform.localEulerAngles = newAngle;
            }
            else
            {
                newAngle = new Vector3(
                    transform.eulerAngles.x + Random.Range(-_rotationOffsetBound.x, _rotationOffsetBound.x),
                    transform.eulerAngles.y + Random.Range(-_rotationOffsetBound.y, _rotationOffsetBound.y),
                    transform.eulerAngles.z + Random.Range(-_rotationOffsetBound.z, _rotationOffsetBound.z));
                
                transform.eulerAngles = newAngle;
            }
        }
    }
}
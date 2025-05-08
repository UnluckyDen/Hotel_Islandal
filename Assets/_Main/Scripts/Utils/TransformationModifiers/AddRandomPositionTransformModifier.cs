using UnityEngine;

namespace _Main.Scripts.Utils.TransformationModifiers
{
    public class AddRandomPositionTransformModifier : BaseTransformModifier
    {
        [SerializeField] private bool _asLocal = true;
        [SerializeField] private Vector3 _positionOffsetBound;

        public override void ApplyModifier()
        {
            Vector3 newAngle;

            if (_asLocal)
            {
                newAngle = new Vector3(
                    transform.localPosition.x + Random.Range(-_positionOffsetBound.x, _positionOffsetBound.x),
                    transform.localPosition.y + Random.Range(-_positionOffsetBound.y, _positionOffsetBound.y),
                    transform.localPosition.z + Random.Range(-_positionOffsetBound.z, _positionOffsetBound.z));

                transform.localPosition = newAngle;
            }
            else
            {
                newAngle = new Vector3(
                    transform.position.x + Random.Range(-_positionOffsetBound.x, _positionOffsetBound.x),
                    transform.position.y + Random.Range(-_positionOffsetBound.y, _positionOffsetBound.y),
                    transform.position.z + Random.Range(-_positionOffsetBound.z, _positionOffsetBound.z));

                transform.position = newAngle;
            }
        }
    }
}
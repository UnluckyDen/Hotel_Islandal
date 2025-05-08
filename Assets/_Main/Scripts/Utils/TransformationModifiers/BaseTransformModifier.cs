using UnityEngine;

namespace _Main.Scripts.Utils.TransformationModifiers
{
    public class BaseTransformModifier : MonoBehaviour
    {
        [SerializeField] protected bool PlayOnAwake = true;

        private void Awake()
        {
            if (PlayOnAwake)
            {
                ApplyModifier();
            }
        }

        public virtual void ApplyModifier()
        {
            
        }
    }
}
using UnityEngine;

namespace _Main.Scripts.Environment.Systems.Radiation
{
    public class RadiationPoint : MonoBehaviour
    {
        [SerializeField] private float _radiation;

        public float Radiation => _radiation;
    }
}
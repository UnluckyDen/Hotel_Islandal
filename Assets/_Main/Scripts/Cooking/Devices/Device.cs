using _Main.Scripts.Interfaces;
using _Main.Scripts.Utils;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices
{
    public class Device : MonoBehaviour, IHoverable
    {
        [SerializeField] private BaseHoverGroup _hoverGroup;
        
        public void OnHoverEnter()
        {
            _hoverGroup.OnHoverEnter();
        }

        public void OnHoverExit()
        {
            _hoverGroup.OnHoverExit();
        }
    }
}
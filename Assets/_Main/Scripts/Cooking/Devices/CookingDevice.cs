using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices
{
    public class CookingDevice : MonoBehaviour, IHoverable, IObjectPlace
    {
        public virtual bool IsEmpty { get; }

        public void OnHoverEnter()
        {
        }

        public void OnHoverExit()
        {
        }

        public virtual bool PlaceMovableObject(IMovableObject movableObject) =>
            false;

        public virtual IMovableObject TakeMovableObject() =>
            null;
    }
}
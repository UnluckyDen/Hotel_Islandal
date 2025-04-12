using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Food
{
    public class Food : MonoBehaviour, IMovableObject, IInteractable
    {
        public void Take()
        {
        }

        public void Place()
        {
        }

        public void OnHoverEnter()
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }

        public void OnHoverExit()
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        public void OnClick()
        {
            throw new System.NotImplementedException();
        }
    }
}
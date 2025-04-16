using _Main.Scripts.Interfaces;
using _Main.Scripts.ScriptableObjects.Receipts;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Cooking
{
    public class CookingDevice : Device, IObjectPlace
    {
        [SerializeField] protected CookingDeviceRecipeSettings _recipeSettings;
        [SerializeField] private Animator _animator;
        
        private static readonly int Cooking1 = Animator.StringToHash("Cooking");
        public virtual bool IsEmpty { get; }

        public virtual bool PlaceMovableObject(IMovableObject movableObject) =>
            false;

        public virtual IMovableObject TakeMovableObject() =>
            null;

        public void PlayCookingAnimation(bool play)
        {
            _animator?.SetBool(Cooking1,play);
        }
    }
}
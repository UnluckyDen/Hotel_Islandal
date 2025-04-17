using _Main.Scripts.Interfaces;
using _Main.Scripts.ScriptableObjects.Receipts;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Cooking
{
    public class CookingDevice : Device, IObjectPlace
    {
        [SerializeField] protected CookingDeviceRecipeSettings _recipeSettings;
        [SerializeField] private Animator _animator;
        
        public virtual bool MayContainMultipleObjects => true;
        public virtual bool IsEmpty { get; }
        
        private static readonly int Cooking = Animator.StringToHash("Cooking");

        public virtual void PlaceMovableObject(IMovableObject movableObject)
        { }

        public virtual IMovableObject TakeMovableObject() =>
            null;

        public void PlayCookingAnimation(bool play)
        {
            _animator?.SetBool(Cooking, play);
        }
    }
}
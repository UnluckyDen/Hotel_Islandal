using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Interfaces;
using _Main.Scripts.ScriptableObjects.Receipts;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices.Cooking
{
    public class CookingDevice : Device, IObjectPlace
    {
        [SerializeField] protected CookingDeviceRecipeSettings _recipeSettings;
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _audioSource;

        private static readonly int Cooking = Animator.StringToHash("Cooking");

        public virtual IMovableObject CurrentMovableObject => null;

        public virtual bool CanApply(IMovableObject movableObject) =>
            movableObject is Food;

        public virtual void PlaceMovableObject(IMovableObject movableObject)
        { }

        public virtual IMovableObject TakeMovableObject() =>
            null;

        public void PlayCookingAnimation(bool play)
        {
            _animator?.SetBool(Cooking, play);
        }
        
        public void PlayCookingSound(bool play)
        {
            if (play)
                _audioSource?.Play();
            else
                _audioSource?.Stop();
        }
    }
}
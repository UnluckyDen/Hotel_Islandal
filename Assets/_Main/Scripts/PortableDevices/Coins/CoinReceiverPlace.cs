using _Main.Scripts.Interfaces;
using _Main.Scripts.Utils;
using JetBrains.Annotations;
using UnityEngine;

namespace _Main.Scripts.PortableDevices.Coins
{
    public class CoinReceiverPlace : MonoBehaviour, IObjectPlace, IHoverable
    {
        [SerializeField] private Transform _place;
        [SerializeField] private BaseHoverGroup _hoverGroup;
        [CanBeNull]
        [SerializeField] private GameObject _startObject;

        private IMovableObject _movableObject;

        public IMovableObject CurrentMovableObject => _movableObject;
        public bool CanApply(IMovableObject movableObject) =>
            movableObject is Coin;

        private void Start()
        {
            if (_startObject == null)
                return;
            
            var movableObject = _startObject.GetComponent<IMovableObject>();
            PlaceMovableObject(movableObject);
            OnHoverExit();
        }

        public void OnHoverEnter()
        {
            _hoverGroup.OnHoverEnter();
            _movableObject?.OnHoverEnter();
        }

        public void OnHoverExit()
        {
            _hoverGroup.OnHoverExit();
            _movableObject?.OnHoverExit();
        }

        public void PlaceMovableObject(IMovableObject movableObject)
        {
            if (_movableObject != null || movableObject == null)
                return;

            _movableObject = movableObject;
            
            _movableObject.transform.SetParent(_place);
            _movableObject.transform.localPosition = Vector3.zero;
            _movableObject.transform.localEulerAngles = Vector3.zero;
            
            OnHoverEnter();

            _movableObject.ToNonInteractive();
        }

        public IMovableObject TakeMovableObject()
        {
            if (_movableObject == null)
                return null;
            
            OnHoverExit();
            
            var movableObject = _movableObject;
            _movableObject = null;

            movableObject.transform.SetParent(null);
            movableObject.ToInteractable();
            
            return movableObject;
        }
    }
}
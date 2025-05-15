using _Main.Scripts.Interfaces;
using _Main.Scripts.PortableDevices.Coins;
using _Main.Scripts.Utils;
using JetBrains.Annotations;
using UnityEngine;

namespace _Main.Scripts.Environment.Elevators
{
    public class CoinReceiverPlace : MonoBehaviour, IObjectPlace, IHoverable
    {
        [SerializeField] private Transform _place;
        [SerializeField] private BaseHoverGroup _hoverGroup;
        [SerializeField] private CoinAperture _coinAperture;
        [CanBeNull]
        [SerializeField] private GameObject _startObject;

        private IMovableObject _movableObject;

        public IMovableObject CurrentMovableObject => _movableObject;
        public bool CanApply(IMovableObject movableObject) =>
            movableObject is Coin;

        public bool IsOpen { get; private set; }

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

        public void OpenAperture()
        {
            _coinAperture.Open();
            IsOpen = true;
        }

        public void CloseAperture()
        {
            _coinAperture.Close();
            IsOpen = false;
        }

        public void DestroyCollectedCoin()
        {
            CloseAperture();
            _coinAperture.StateChanged += b =>
            {
                if (_movableObject != null)
                {
                    Destroy(_movableObject.transform.gameObject);
                    _movableObject = null;
                }
            };
        }
    }
}
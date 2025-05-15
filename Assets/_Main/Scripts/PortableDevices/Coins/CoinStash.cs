using _Main.Scripts.Interfaces;
using _Main.Scripts.Utils;
using _Main.Scripts.Utils.GameObjectGroups;
using JetBrains.Annotations;
using UnityEngine;

namespace _Main.Scripts.PortableDevices.Coins
{
    public class CoinStash : MonoBehaviour, IObjectPlace, IHoverable
    {
        [SerializeField] private MovableObjectGroupPlace _coinsGroup;
        [SerializeField] private BaseHoverGroup _hoverGroup;
        [CanBeNull]
        [SerializeField] private GameObject _startObject;
        
        public IMovableObject CurrentMovableObject => _coinsGroup.LastMovableObject;
        
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
            _coinsGroup.OnHoverEnter();
        }

        public void OnHoverExit()
        {
            _hoverGroup.OnHoverExit();
            _coinsGroup.OnHoverExit();
        }

        public void PlaceMovableObject(IMovableObject movableObject)
        {
            if (movableObject == null)
                return;

            _coinsGroup.InGroup(movableObject);
        }

        public IMovableObject TakeMovableObject()
        {
            if (CurrentMovableObject == null)
                return null;
            
            IMovableObject movableObject = _coinsGroup.OutGroup();
            
            return movableObject;
        }
    }
}
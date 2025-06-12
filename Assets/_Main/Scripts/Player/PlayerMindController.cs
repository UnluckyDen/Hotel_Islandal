using _Main.Scripts.PortableDevices;
using _Main.Scripts.Utils.GlobalEvents.Events;
using UnityEngine;
using EventProvider = _Main.Scripts.Utils.GlobalEvents.EventProvider;

namespace _Main.Scripts.Player
{
    public class PlayerMindController : MonoBehaviour
    {
        [SerializeField] private int _maxMind;
        [SerializeField] private int _startMind;
        [Space] 
        [SerializeField] private MindView _mindView;

        private EventProvider _eventProvider;
        private int _currentMind;

        public void Init(EventProvider eventProvider)
        {
            _eventProvider = eventProvider;

            _currentMind = _startMind;
            _mindView.SetMindCount(_currentMind);
            
            _eventProvider.Subscribe<ScreamerShowedEvent>(OnScreamerShowed);
        }

        public void Destruct()
        {
            _eventProvider.UnSubscribe<ScreamerShowedEvent>(OnScreamerShowed);
        }

        public void OnScreamerShowed(ScreamerShowedEvent screamerShowedEvent)
        {
            _currentMind -= screamerShowedEvent.MindDamage;
            
            if (_mindView != null)
                _mindView.SetMindCount(_currentMind);
        }
    }
}

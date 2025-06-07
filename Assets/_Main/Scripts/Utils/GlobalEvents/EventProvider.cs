using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.Utils.GlobalEvents
{
    public class EventProvider : MonoBehaviour
    {
        private readonly Dictionary<Type, ArrayList> _globalEvents = new ();

        public static EventProvider Instance;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }

            Destroy(gameObject);
        }

        public void Invoke<T>(T arg) where T : IEvent
        {
            if (!_globalEvents.ContainsKey(typeof(T))) 
                return;
            
            ArrayList tmp = new ArrayList(_globalEvents[typeof(T)]);
            foreach (Action<T> action in tmp)
                action.Invoke(arg);
        }
        
        public void Subscribe<T>(Action<T> action) where T : IEvent
        {
            if (!_globalEvents.ContainsKey(typeof(T)))
                _globalEvents.Add(typeof(T), new ArrayList());

            _globalEvents[typeof(T)].Add(action);
        }

        public void UnSubscribe<T>(Action<T> action) where T : IEvent
        {
            if (_globalEvents.ContainsKey(typeof(T)))
                _globalEvents[typeof(T)].Remove(action);
            else
                Debug.LogWarning("EventClass not presented in dictionary");
        }
    }
}
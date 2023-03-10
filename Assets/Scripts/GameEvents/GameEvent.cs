using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmGame
{
    [CreateAssetMenu(menuName = "GameEvent/New GameEvent", fileName = "New GameEvent")]
    public class GameEvent : ScriptableObject
    {
        public List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise(Component sender, Object data)
        {
            listeners.ForEach(listener => listener.OnEventRaised(sender, data));
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}

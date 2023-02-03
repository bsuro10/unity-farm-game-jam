using UnityEngine;
using UnityEngine.Events;

namespace FarmGame
{
    [System.Serializable]
    public class CustomGameEvent : UnityEvent<Component, object> { }

    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private CustomGameEvent response;

        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(Component sender, Object data)
        {
            response.Invoke(sender, data);
        }
    }
}

using UnityEngine;

namespace FarmGame
{
    public class Interactable : MonoBehaviour
    {
        public virtual void Interact()
        {
            Debug.Log("Interacting with: " + transform.name);
        }

    }
}

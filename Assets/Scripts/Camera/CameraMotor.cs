using UnityEngine;

namespace FarmGame
{
    public class CameraMotor : MonoBehaviour
    {
        [SerializeField] private Transform focusOn;
        [SerializeField] private float boundX = 0.25f;
        [SerializeField] private float boundY = 0.15f;

        void LateUpdate()
        {
            Vector3 delta = Vector3.zero;

            // The difference between where the player is at and where the camera is at
            float deltaX = focusOn.position.x - transform.position.x;
            if (deltaX > boundX || deltaX < -boundX)
            {
                if (deltaX > 0)
                {
                    delta.x = deltaX - boundX;
                }
                else
                {
                    delta.x = deltaX + boundX;
                }
            }

            float deltaY = focusOn.position.y - transform.position.y;
            if (deltaY > boundY || deltaY < -boundY)
            {
                if (deltaY > 0)
                {
                    delta.y = deltaY - boundY;
                }
                else
                {
                    delta.y = deltaY + boundY;
                }
            }

            transform.position += delta;
        }
    }
}
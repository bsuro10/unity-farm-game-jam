using UnityEngine;

namespace FarmGame
{
    public class PlayerController : CharacterBasicController
    {

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        void Update()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            moveDelta = new Vector3(horizontalInput, verticalInput, 0);
            FlipCharacterAccordingToWalkingDirection(moveDelta);
            MovePlayer(moveDelta);
        }

        private void FlipCharacterAccordingToWalkingDirection(Vector3 moveDelta)
        {
            if (moveDelta.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (moveDelta.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }

        private void MovePlayer(Vector3 moveDelta)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, moveDelta.y * Time.deltaTime, 0);
        }
    }
}

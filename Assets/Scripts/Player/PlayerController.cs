using UnityEngine;

namespace FarmGame
{
    public class PlayerController : CharacterBasicController
    {
        [SerializeField] private LayerMask characterLayerMask;
        [SerializeField] private LayerMask BlockingLayerMask;

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
            MovePlayerVertical();
            MovePlayerHorizontal();
        }

        private void MovePlayerVertical()
        {
            RaycastHit2D hit;
            hit = Physics2D.BoxCast(transform.position,
                boxCollider.size,
                0,
                new Vector2(0, moveDelta.y),
                Mathf.Abs(moveDelta.y * Time.deltaTime),
                characterLayerMask.value | BlockingLayerMask.value);

            if (hit.collider == null)
                transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        private void MovePlayerHorizontal()
        {
            RaycastHit2D hit;
            hit = Physics2D.BoxCast(transform.position,
                boxCollider.size,
                0,
                new Vector2(moveDelta.x, 0),
                Mathf.Abs(moveDelta.x * Time.deltaTime),
                characterLayerMask.value | BlockingLayerMask.value);

            if (hit.collider == null)
                transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }

    }
}

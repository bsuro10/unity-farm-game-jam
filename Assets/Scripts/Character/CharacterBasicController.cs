using UnityEngine;

namespace FarmGame
{
    [RequireComponent(
        typeof(BoxCollider2D),
        typeof(Rigidbody2D),
        typeof(Animator)
    )]
    public class CharacterBasicController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float speed = 5f;

        [Header("Game Layers")]
        [SerializeField] private LayerMask characterLayerMask;
        [SerializeField] private LayerMask blockingLayerMask;

        [Header("For non 2D movement instead of 4D")]
        [SerializeField] private bool flipSpriteToChangeDirection;

        public bool isWalking { get; protected set; }
        public Vector2 moveDelta { get; protected set; }
        protected BoxCollider2D boxCollider;
        protected Rigidbody2D body;

        protected virtual void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            body = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 newMoveDelta)
        {
            if (newMoveDelta == Vector2.zero)
            {
                isWalking = false;
            }
            else
            {
                isWalking = true;
                moveDelta = newMoveDelta;
                body.MovePosition(body.position + moveDelta.normalized * speed * Time.fixedDeltaTime);

                if (flipSpriteToChangeDirection)
                    FlipCharacterAccordingToMovingDirection(moveDelta);
            }
        }

        private void FlipCharacterAccordingToMovingDirection(Vector2 newMoveDelta)
        {
            if (moveDelta.x > 0)
            {
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
            else if (moveDelta.x < 0)
            {
                transform.localScale = new Vector2(-1, transform.localScale.y);
            }
        }

    }

}

using UnityEngine;

namespace FarmGame
{
    public class PlayerController : CharacterBasicController
    {
        [Header("Game Layers")]
        [SerializeField] private LayerMask characterLayerMask;
        [SerializeField] private LayerMask BlockingLayerMask;

        [Header("Interactions")]
        [SerializeField] private float interactionRadius = 0.2f;

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
            if (Input.GetButtonDown("Interact"))
                CheckInteraction();
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

        private void CheckInteraction()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRadius);

            if (hits.Length > 0)
            {
                foreach (Collider2D hit in hits)
                {
                    if (hit.transform.GetComponent<Interactable>())
                    {
                        hit.transform.GetComponent<Interactable>().Interact();
                        return;
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }

    }
}

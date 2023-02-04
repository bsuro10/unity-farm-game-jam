using UnityEngine;

namespace FarmGame
{
    public class PlayerController : CharacterBasicController
    {

        #region Singleton
        public static PlayerController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        [Header("Movement")]
        [SerializeField] private float speed = 5f;

        [Header("Game Layers")]
        [SerializeField] private LayerMask characterLayerMask;
        [SerializeField] private LayerMask BlockingLayerMask;

        [Header("Interactions")]
        [SerializeField] private float interactionRadius = 0.2f;
        [SerializeField] public Transform playerTransform;

        public bool isInDialogue { get; set; }

        protected override void Start()
        {
            base.Start();
        }

        void Update()
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (isInDialogue)
                    DialogueManager.Instance.DisplayNextSentence();
                else
                    CheckInteraction();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CropsManager.Instance.Harvest(playerTransform.position);
            }
        }

        private void FixedUpdate()
        {
            if (!isInDialogue)
            {
                float horizontalInput = Input.GetAxisRaw("Horizontal");
                float verticalInput = Input.GetAxisRaw("Vertical");
                moveDelta = new Vector3(horizontalInput * speed, verticalInput * speed, 0);
                isWalking = moveDelta != Vector3.zero;
                FlipCharacterAccordingToWalkingDirection(moveDelta);
                MovePlayer(moveDelta);
            }
            else
            {
                isWalking = false;
            }
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

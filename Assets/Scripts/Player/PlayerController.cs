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

        [Header("Interactions")]
        [SerializeField] private float interactionRadius = 0.2f;
        [SerializeField] public Transform playerTransform;

        private Vector2 moveDeltaInput; 

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

            moveDeltaInput.x = Input.GetAxisRaw("Horizontal");
            moveDeltaInput.y = Input.GetAxisRaw("Vertical");
            
        }

        private void FixedUpdate()
        {
            if (!isInDialogue)
            {
                Move(moveDeltaInput);
            }
            else
            {
                isWalking = false;
            }
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

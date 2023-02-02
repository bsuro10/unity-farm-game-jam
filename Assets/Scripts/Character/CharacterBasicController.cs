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
        public Vector3 moveDelta { get; protected set; }
        protected BoxCollider2D boxCollider;
        protected Rigidbody2D body;

        protected virtual void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            body = GetComponent<Rigidbody2D>();
        }

        protected virtual void Start()
        {
            moveDelta = Vector3.zero;
        }
    }

}

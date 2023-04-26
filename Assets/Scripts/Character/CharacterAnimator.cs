using UnityEngine;

namespace FarmGame
{
    [RequireComponent(typeof(CharacterBasicController), typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        private CharacterBasicController characterController;
        private Animator animator;

        protected virtual void Start()
        {
            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterBasicController>();
        }

        protected virtual void Update()
        {
            animator.SetFloat("Horizontal", characterController.moveDelta.x);
            animator.SetFloat("Vertical", characterController.moveDelta.y);
            animator.SetBool("isWalking", characterController.isWalking);
        }
    }
}

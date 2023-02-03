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
            animator.SetBool("isWalking", characterController.isWalking);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmGame {

    [RequireComponent(
        typeof(CharacterBasicController)
    )]
    public class NpcPatrol : MonoBehaviour
    {
        [SerializeField] public Transform[] waypoints;

        private CharacterBasicController characterBasicController;
        private int currentWaypointIndex;
        private Vector3 target;
        private bool canWalkToNextWaypoint = true;

        void Start()
        {
            characterBasicController = GetComponent<CharacterBasicController>();
            if (waypoints.Length == 0)
                canWalkToNextWaypoint = false;
        }

        void FixedUpdate()
        {
            if (canWalkToNextWaypoint && !characterBasicController.isInDialogue)
            {
                WalkTowardsNextWayPoint();
            }
            else
            {
                characterBasicController.Move(Vector2.zero);
            }
        }

        private void WalkTowardsNextWayPoint()
        {
            target = waypoints[currentWaypointIndex].position;
            Vector2 movingDirection = (target - transform.position).normalized;
            float distance = Vector2.Distance(target, transform.position);
            if (distance <= 0.1f)
            {
                currentWaypointIndex = ++currentWaypointIndex % waypoints.Length;
                StartCoroutine(WaitOnWaypoint());
            }
            characterBasicController.Move(movingDirection);
        }

        IEnumerator WaitOnWaypoint()
        {
            canWalkToNextWaypoint = false;
            yield return new WaitForSeconds(5);
            canWalkToNextWaypoint = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runtime.Player;

namespace Runtime.Movables
{
    public class Climable : MonoBehaviour, IBlock
    {
        public Transform playerClimbPosition;
        public Transform playerTopPosition;
        public BoxCollider topCollider;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                topCollider.enabled = false;
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<PlayerMovement>().MoveToOverTime(playerTopPosition.position, 1, () => topCollider.enabled = true);
            }
        }

        public void OnUpdate()
        {
            GameControl.Instance.onBlockUpdate.Invoke();
            GameControl.Instance.BlockUpdateNextFrame();
        }
    }
}


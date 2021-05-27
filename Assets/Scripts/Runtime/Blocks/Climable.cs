using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runtime.Player;

namespace Runtime.Movables
{
    public class Climable : MonoBehaviour, IBlock
    {
        public Transform playerClimbPosition;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<PlayerMovement>().MoveTo(playerClimbPosition.position);
            }
        }

        public void OnUpdate()
        {
            GameControl.Instance.onBlockUpdate.Invoke();
            GameControl.Instance.BlockUpdateNextFrame();
        }
    }
}


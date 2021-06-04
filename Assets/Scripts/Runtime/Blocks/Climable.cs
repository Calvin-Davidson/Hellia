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
                Vector3 targetPos = player.transform.position + new Vector3(0, 4, 0);
                if (player.TryGetComponent(out PlayerMovement playerMovement))
                {
                    float prevGravity = playerMovement.Gravity;
                    playerMovement.Gravity = 0f;
                    
                    playerMovement.MoveTo(playerClimbPosition.position);
                    player.transform.LookAt(transform.position);
                    playerMovement.MoveToOverTime(targetPos, 1,
                        () =>
                        {
                            playerMovement.Gravity = prevGravity;
                            playerMovement.MoveToOverTime(player.transform.position + transform.right, 5, null, false);
                        }, false);
                }
            }
        }

        public void OnUpdate()
        {
            GameControl.Instance.onBlockUpdate.Invoke();
            GameControl.Instance.BlockUpdateNextFrame();
        }
    }
}
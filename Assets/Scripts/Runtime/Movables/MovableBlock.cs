using System;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Movables
{
    public class MovableBlock : MonoBehaviour
    {
        [SerializeField] private Vector3 blockSize;
        [SerializeField] private float cornerTolerance;
        
        [SerializeField] private LayerMask pushableByLayers;


        public void TryPushTo(Vector3 direction)
        {
            int colliders = Physics.OverlapBoxNonAlloc(transform.position + direction, blockSize, new Collider[1]);
            if (colliders == 0)
                transform.position += direction;
        }

        private void OnCollisionStay(Collision other)
        {
            if (((pushableByLayers.value & (1 << other.gameObject.layer)) > 0))
            {
                Vector3 contactPoint = other.contacts[0].point;
                Vector3 contactDirection = transform.position - contactPoint;

                float xDifference = contactDirection.x;
                float zDifference = contactDirection.z;

                // you are now allowed to push the corners.
                if (Math.Abs(math.abs(xDifference) - math.abs(zDifference)) < cornerTolerance) return;

                // Collision was on the X axis
                if (math.abs(xDifference) > math.abs(zDifference))
                {
                    if (contactDirection.x < 0)
                        TryPushTo(new Vector3(-4, 0, 0));
                    else
                        TryPushTo(new Vector3(4, 0, 0));
                }
                else
                {
                    if (contactDirection.z < 0)
                        TryPushTo(new Vector3(0, 0, -4));
                    else
                        TryPushTo(new Vector3(0, 0, 4));
                }
            }
        }
    }
}
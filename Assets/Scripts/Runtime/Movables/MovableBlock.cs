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
        [SerializeField] private bool shouldObjectLookAtMe = false;
        [SerializeField, Range(1, 2)] private int maxPushableAtOnce = 1;


        private static readonly float RotationTolerance = 20;
        private static readonly int MoveDistance = 4;

        public bool TryPushTo(Vector3 direction, GameObject collidedObject)
        {
            Collider[] colliders = new Collider[1];
            int collisionCount = Physics.OverlapBoxNonAlloc(transform.position + direction, blockSize, colliders);
            if (collisionCount == 0 && !shouldObjectLookAtMe)
            {
                transform.position += direction;
                return true;
            }
            else if (collisionCount == 0 && IsLookingAtMe(collidedObject, direction))
            {
                transform.position += direction;
                return true;
            }

            // Multiple pushable 
            if (collisionCount >= 1)
            {
                if (maxPushableAtOnce <= 1)
                    return false;

                MovableBlock result = colliders[0].gameObject.GetComponent<MovableBlock>();
                if (result != null)
                {
                    bool canBePushed = result.CanBePushed(direction);
                    if (canBePushed)
                    {
                        colliders[0].gameObject.transform.position += direction;
                        transform.position += direction;
                    }
                }
            }

            return false;
        }

        public bool CanBePushed(Vector3 direction)
        {
            int collisionCount = Physics.OverlapBoxNonAlloc(transform.position + direction, blockSize, new Collider[1]);
            return (collisionCount == 0);
        }
        private bool IsLookingAtMe(GameObject other, Vector3 pushDirection)
        {
            Vector3 colliderRotation = other.transform.rotation.eulerAngles;
            if (pushDirection.x >= MoveDistance &&
                IsBetweenRotation(colliderRotation.y, 90 - RotationTolerance, 90 + RotationTolerance))
            {
                return true;
            }

            if (pushDirection.x <= -MoveDistance &&
                IsBetweenRotation(colliderRotation.y, 270 - RotationTolerance, 270 + RotationTolerance))
            {
                return true;
            }

            if (pushDirection.z >= MoveDistance &&
                IsBetweenRotation(colliderRotation.y, 360 - RotationTolerance, 360 + RotationTolerance))
            {
                return true;
            }

            if (pushDirection.z <= -MoveDistance &&
                IsBetweenRotation(colliderRotation.y, 180 - RotationTolerance, 180 + RotationTolerance))
            {
                return true;
            }

            return false;
        }

        private bool IsBetweenRotation(float value, float min, float max)
        {
            float fullRotationFix = Math.Abs(value - 360);
            return (value > min && value < max || fullRotationFix > min && fullRotationFix < max);
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
                        TryPushTo(new Vector3(-MoveDistance, 0, 0), other.gameObject);
                    else
                        TryPushTo(new Vector3(MoveDistance, 0, 0), other.gameObject);
                }
                else
                {
                    if (contactDirection.z < 0)
                        TryPushTo(new Vector3(0, 0, -MoveDistance), other.gameObject);
                    else
                        TryPushTo(new Vector3(0, 0, MoveDistance), other.gameObject);
                }
            }
        }
    }
}
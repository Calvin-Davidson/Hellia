using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private float gravity = 9.81f;
        [SerializeField] private float playerSpeed = 5.0f;

        [SerializeField] private bool relativeToCamera;
        [SerializeField] private Transform cameraTransform;

        private Vector3 _playerVelocity;
        private bool _isGrounded;

        public static UnityEvent<Vector3> PlayerMoveEvent = new UnityEvent<Vector3>();

        void Update()
        {
            Vector3 currentPos = transform.position;
            
            _isGrounded = controller.isGrounded;
            if (_isGrounded && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            if (!relativeToCamera) return;


            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");

            var forward = cameraTransform.forward;
            var right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;


            controller.Move(desiredMoveDirection * (Time.deltaTime * playerSpeed));

            if (desiredMoveDirection != Vector3.zero)
            {
                gameObject.transform.forward = desiredMoveDirection;
            }

            _playerVelocity.y += -gravity * Time.deltaTime;
            controller.Move(_playerVelocity * Time.deltaTime);

            PlayerMoveEvent?.Invoke(transform.position - currentPos);
        }
    }
}
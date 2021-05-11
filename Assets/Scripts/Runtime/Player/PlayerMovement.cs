using UnityEngine;

namespace Runtime.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        public  CharacterController controller;
        private Vector3 _playerVelocity;
        private bool _isGrounded;
        private float playerSpeed = 5.0f;
        private float gravityValue = -9.81f;

        void Update()
        {
            _isGrounded = controller.isGrounded;
            if (_isGrounded && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * (Time.deltaTime * playerSpeed));

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            _playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(_playerVelocity * Time.deltaTime);
        }
    }
}
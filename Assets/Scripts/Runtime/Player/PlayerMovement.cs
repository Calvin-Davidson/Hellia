using System;
using System.Collections;
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
        [SerializeField] private string inputX = "Horizontal";
        [SerializeField] private string inputY = "Vertical";

        private Animator _animator;
        private static int walkingAnimationID = Animator.StringToHash("IsWalking");

        private Vector3 _playerVelocity;
        private bool _isGrounded;

        public static UnityEvent<Vector3> PlayerMoveEvent = new UnityEvent<Vector3>();

        private void Awake()
        {
            controller = this.GetComponent<CharacterController>();
            _animator = this.GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            Vector3 prevPosition = transform.position;

            _isGrounded = controller.isGrounded;
            if (_isGrounded && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            if (!relativeToCamera) return;

            float horizontalAxis = Input.GetAxis(inputX);
            float verticalAxis = Input.GetAxis(inputY);

            var forward = cameraTransform.forward;
            var right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;

            controller.Move(desiredMoveDirection * (Time.deltaTime * playerSpeed));

            _animator.SetBool(walkingAnimationID, false);
            if (desiredMoveDirection != Vector3.zero)
            {
                gameObject.transform.forward = desiredMoveDirection;
                _animator.SetBool(walkingAnimationID, true);
            }

            _playerVelocity.y -= gravity * Time.deltaTime;
            controller.Move(_playerVelocity * Time.deltaTime);

            PlayerMoveEvent?.Invoke(transform.position - prevPosition);
        }

        public void MoveTo(Vector3 newPosition)
        {
            Vector3 prevPosition = transform.position;
            Vector3 diffVector = newPosition - prevPosition;
            controller.Move(diffVector);
            PlayerMoveEvent?.Invoke(transform.position - prevPosition);
        }

        public void MoveToOverTime(Vector3 newPosition, float speed, Action onComplete, bool canStillMove = true)
        {
            StartCoroutine(MovePlayerOverTime(newPosition, speed, onComplete, canStillMove));
        }

        private IEnumerator MovePlayerOverTime(Vector3 newPosition, float speed, Action onComplete, bool canStillMove = true)
        {
            float percent = 0f;
            Vector3 startPosition = transform.position;

            while (percent < 1)
            {
                percent += Time.deltaTime * speed;
                if (percent > 1) percent = 1;
                MoveTo(Vector3.Lerp(startPosition, newPosition, percent));
                yield return null;
            }
            
            onComplete?.Invoke();
        }

        public float Gravity
        {
            get => gravity;
            set => gravity = value;
        }
    }
}
using UnityEngine;

namespace CrimsofallTechnologies.XR
{
    public class PlayerMotor : MonoBehaviour
    {
        public float movementSpeed = 5f;
        public float runSpeedMultiplier = 2f;
        public float wallMoveStep;
        public float grappleSpeed;
        public float pushForce = 10.0f;
        public LayerMask groundMask;

        public Camera cam;
        public Transform bodyRotationHelper; // This is the object that defines the forward/right of the player body controller.

        [Space]
        public float downForce = 10f;
        public float jumpForce = 15f;
        public Transform groundCheckPoint;

        private CharacterController characterController;
        private Vector3 velocity;
        private bool isGrounded;

        private Vector3 startPos;
        public bool hasJetpack { get; set; }

        void Start()
        {   
            startPos = transform.position;

            // Initialize the CharacterController
            characterController = GetComponent<CharacterController>();

            if (characterController == null)
            {
                Debug.LogError("CharacterController component is missing on the PlayerMotor GameObject.");
            }
        }

        void Update()
        {
            // Check if the player is grounded
            isGrounded = Physics.CheckSphere(groundCheckPoint.position, 0.4f, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // Reset downward velocity when grounded
            }

            // Apply gravity
            velocity.y += Physics.gravity.y * downForce * Time.deltaTime;

            // Apply vertical velocity
            characterController.Move(velocity * Time.deltaTime);

            if(transform.position.y <= -50f) {
                Teleport(startPos);
            }
        }

        public void Teleport(Vector3 pos)
        {
            characterController.enabled = false;
            transform.position = pos;
            characterController.enabled = true;
        }

        public void FeedInput(Vector2 input)
        {
            // Handle movement input relative to the bodyRotationHelper's forward and right directions
            Vector3 move = (bodyRotationHelper.forward * input.y + bodyRotationHelper.right * input.x).normalized;
            characterController.Move(move * movementSpeed * Time.deltaTime);
        }

        public void Jump()
        {
            if (isGrounded) // Ensure the player can only jump when grounded
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody rb = hit.collider.attachedRigidbody;
            if(rb!=null && !rb.isKinematic)
            {
                Vector3 pushDir = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);
                rb.AddForce(pushDir * pushForce, ForceMode.Force);
            }
        }
    }
}
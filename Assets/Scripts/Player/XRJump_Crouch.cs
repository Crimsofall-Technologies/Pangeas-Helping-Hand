using UnityEngine;
using UnityEngine.InputSystem;

public class XRJump_Crouch : MonoBehaviour
{
    public InputActionReference crouchInput, jumpRightInput, jumpLeftInput;
    public float jumpHeight, gravity;

    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool IsGrounded, canJump=true;

    private void Start() {
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable() {
        jumpLeftInput.action.performed += Jumping;
        jumpRightInput.action.performed += Jumping;  
    }

    private void OnDisable() {
        jumpLeftInput.action.performed -= Jumping; 
        jumpRightInput.action.performed -= Jumping;      
    }

    private void Jumping(InputAction.CallbackContext obj)
    {
        if (!characterController.isGrounded) 
            return;
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }

    private void Update()
    {
        if(!canJump) return; //while not jumping disable jumping to apply velocity to controller too

        IsGrounded = Physics.CheckSphere(transform.position, 0.1f);
        if (IsGrounded && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void ToggleJump(bool value)
    {
        canJump = value;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}

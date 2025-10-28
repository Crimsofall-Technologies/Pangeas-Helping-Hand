using UnityEngine;
using UnityEngine.InputSystem;

namespace CrimsofallTechnologies.XR
{
    public class PlayerInput : MonoBehaviour
    {
        public PlayerMotor playerMotor;

        [Space]
        public InputActionProperty moveInput;
        public InputActionProperty jumpInput;
        public InputActionProperty interactInput;
        public InputActionProperty grabInput, enterPlaceInput;

        private bool cursorLocked;
        private PlayerHealth health;

        private void Start()
        {
            health = GetComponent<PlayerHealth>();
        }

        private void Update()
        {
            if(health.IsDead)
            {
                playerMotor.FeedInput(Vector2.zero); //stop movement.
                return;
            }

            Vector2 i = moveInput.action.ReadValue<Vector2>();
            //Vector3 input = new Vector3(i.x, Input.GetAxis("UpDown"), i.y);
            
            //Player Movement
            if (playerMotor.enabled) 
            {
                //disabled for XR
                /*if (input.magnitude != 0f)
                {
                    inputHeldTime = minMoveStepTime;
                }
                else if (inputHeldTime > 0f)
                {
                    inputHeldTime -= Time.deltaTime;
                    if (inputHeldTime <= 0f)
                    {
                        input = Vector3.zero;
                        inputHeldTime = 0f;
                    }
                }*/

                playerMotor.FeedInput(new Vector2(i.x, i.y).normalized);

                if (jumpInput.action.WasPressedThisFrame())
                {
                    playerMotor.Jump();
                }
            }

            //Locking/Unlocking Cursor (PC only)
            if (Input.GetKeyDown(KeyCode.Escape) && cursorLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                cursorLocked = false;
            }

            if (Input.GetMouseButton(0) && !cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                cursorLocked = true;
            }
        }
    }
}